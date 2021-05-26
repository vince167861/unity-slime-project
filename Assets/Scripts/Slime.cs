using UnityEngine;

/**
 * Pickup potion: line 142
 * Consume potion: line 83
 */

public class Slime : Entity
{
    public Slime() : base(6) { }

    public GameObject Bomb;
    public int bulletDirection = 1; // -1 = left, 1 = right
    float moveSpeed = 120f; // main character movement speed
    readonly float jumpStrength = 2e4f; // main character jump strenght
    readonly float dropStrength = 100f; // main character drop strenght

    Animator animator;
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRender;
    public bool isTouchingGround = false, allowMove = true;

    float immuableTime = 0;
    int life = 6;
    public static int potionCount = 0;
    public static int potionMax = 100;

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
                // Control camera postion
                MainCameraHandler.targetPosition = new Vector3(transform.position.x, transform.position.y, -10);
                // Set Slime to follow physics engine
                rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
                // Response to keyboard input
                animator.SetBool("right", Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));
                animator.SetBool("left", Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
                animator.SetBool("jump", Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W));
                animator.SetBool("crouch", Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S));
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S)) rigidbody2d.AddForce(new Vector2(0, -dropStrength));
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isTouchingGround)
                {
                    MainCameraHandler.allSound = 2;
                    rigidbody2d.AddForce(new Vector2(0, jumpStrength));
                }
                if (Input.GetKey(KeyCode.A) && allowMove)
                {
                    rigidbody2d.AddForce(new Vector2(-moveSpeed * (isTouchingGround ? 1f : 0.5f), 0));
                    bulletDirection = -1;
                }
                if (Input.GetKey(KeyCode.D) && allowMove)
                {
                    rigidbody2d.AddForce(new Vector2(moveSpeed * (isTouchingGround ? 1f : 0.5f), 0));
                    bulletDirection = 1;
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Vector3 pos = transform.position + new Vector3(bulletDirection * 5, 0, 0);
                    Instantiate(Bomb, pos, transform.rotation).GetComponent<BulletHandler>().moveSpeed *= bulletDirection;
                }
                if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) allowMove = true;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (potionCount > 0)
                    {
                        potionCount--;
                    }
                }
                break;
            case GameGlobalController.GameState.Pause:
                rigidbody2d.bodyType = RigidbodyType2D.Static;
                break;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.collider.tag)
        {
            case "Enemy":
                UnderAttack(col.collider);
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Walls":
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && Input.GetKey(KeyCode.A) && allowMove)
                {
                    MainCameraHandler.allSound = 2;
                    rigidbody2d.AddForce(new Vector2(100 * moveSpeed, jumpStrength));
                    animator.Play("slime_jump");
                    allowMove = false;
                }
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && Input.GetKey(KeyCode.D) && allowMove)
                {
                    MainCameraHandler.allSound = 2;
                    rigidbody2d.AddForce(new Vector2(100 * -moveSpeed, jumpStrength));
                    allowMove = false;
                    animator.Play("slime_jump");
                }
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
        }
        Debug.Log(collision.tag);
    }

    void UnderAttack(Collider2D col)
    {
        if (!GameGlobalController.isAnimation)
        {
            SlimeLifeCanvas.Shake();
            if (!Suffer(col.GetComponent<Attackable>().AttackDamage, true))
                transform.position = new Vector3(-5, -5, -10);
            SlimeLifeCanvas.life = health;
        }
    }
}
