using UnityEngine;

/**
 * Pickup potion: line 142
 * Consume potion: line 83
 */

public class Slime : Entity
{
    public Slime() : base(6) { }

    public GameObject Bomb;
    float moveSpeed = 120f; // main character movement speed
    readonly float jumpStrength = 2e4f; // main character jump strenght
    readonly float dropStrength = 100f; // main character drop strenght

    static Animator animator;
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRender;
    public bool isTouchingGround = false, bouncable = false, allowMove = false;

    float immuableTime = 0;
    int life = 6;
    public static int potionCount = 0;
    public static int potionMax = 100;
    public int keyCount = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Playing:
            case GameGlobalController.GameState.Lobby:
                // Control immuable
                if (immuableTime <= 0) spriteRender.color = new Color(255, 255, 255, 90);
                if (life > health && life != 7)
                {
                    immuableTime = 0.2f;
                    spriteRender.color = new Color(255, 0, 0, 90);
                }
                life = health;
                immuableTime -= Time.deltaTime;
                // Control camera postion, except for the time in the welcome screen
                if (!(GameGlobalController.currentLevel == 0 && GameGlobalController.isLobby))
                    MainCameraHandler.targetPosition = new Vector3(transform.position.x, transform.position.y, -10);
                // Set Slime to follow physics engine
                rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
                // Response to keyboard input
                if (bouncable && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
                {
                    MainCameraHandler.allSound = 2;
                    rigidbody2d.AddForce(new Vector2(200 * moveSpeed, jumpStrength));
                    animator.Play("Jump Right");
                    direction = 1;
                    allowMove = bouncable = false;
                }
                if (bouncable && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
                {
                    MainCameraHandler.allSound = 2;
                    rigidbody2d.AddForce(new Vector2(200 * -moveSpeed, jumpStrength));
                    animator.Play("Jump Left");
                    direction = -1;
                    allowMove = bouncable = false;
                }
                if (allowMove)
                {
                    animator.SetBool("right", Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));
                    animator.SetBool("left", Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
                    animator.SetBool("jump", Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W));
                    animator.SetBool("crouch", Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S));
                }
                if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && allowMove)
                {
                    rigidbody2d.AddForce(new Vector2(-moveSpeed * (isTouchingGround ? 1f : 0.5f), 0));
                    direction = -1;
                }
                if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && allowMove)
                {
                    rigidbody2d.AddForce(new Vector2(moveSpeed * (isTouchingGround ? 1f : 0.5f), 0));
                    direction = 1;
                }
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S))
                    rigidbody2d.AddForce(new Vector2(0, -dropStrength));
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isTouchingGround)
                {
                    MainCameraHandler.allSound = 2;
                    rigidbody2d.AddForce(new Vector2(0, jumpStrength));
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    MainCameraHandler.allSound = 4;
                    Vector3 pos = transform.position + new Vector3(direction * 5, 0, 0);
                    Instantiate(Bomb, pos, transform.rotation).GetComponent<BulletHandler>().moveSpeed *= direction;
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (potionCount > 0)
                    {
                        potionCount--;
                    }
                }
                if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                    allowMove = true;
                break;
            case GameGlobalController.GameState.Pause:
                rigidbody2d.bodyType = RigidbodyType2D.Static;
                break;
        }
    }
    public static void disappear()
    {
        animator.Play("Slime_disappear");
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.collider.tag)
        {
            case "Enemy":
                UnderAttack(col.collider);
                break;
            case "Ground":
                bouncable = false;
                break;
            case "Walls":
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                    bouncable = true;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Enemy":
                UnderAttack(col);
                break;
            case "EventTrigger":
                Animation.handler.trigger(col.GetComponent<TriggerHandler>().triggerId);
                Destroy(col.gameObject);
                break;
            case "Mushroom":
                col.GetComponent<MushroomHandler>().trigger();
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Potion":
                Destroy(collision.gameObject);
                potionCount++;
                break;
             case "Key":
                MainCameraHandler.allSound = 5;
                Destroy(collision.gameObject);
                keyCount++;
                break;   
        }
    }

    void UnderAttack(Collider2D col)
    {
        if (!GameGlobalController.isAnimation && immuableTime <= 0)
        {
            SlimeLifeCanvas.Shake();
            if (!Suffer(col.GetComponent<Attackable>().AttackDamage, true))
            {
                transform.position = new Vector3(-5, -5, -10);
                GameGlobalController.BadEnd();
            }
            SlimeLifeCanvas.life = health;
        }
    }
}
