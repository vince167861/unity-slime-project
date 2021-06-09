#pragma warning disable CS0108
using UnityEngine;

/**
 * Pickup potion: line 142
 * Consume potion: line 83
 */

public class Slime : Entity
{
    public static Slime instance;
    public static Animator animator;
    public static Rigidbody2D rigidbody2d;
    public static SpriteRenderer spriteRender;
    public static Transform transform;


    public GameObject Bomb;
    readonly float moveSpeed = 120f, jumpStrength = 2e4f, dropStrength = 100f;

    public static bool isTouchingGround = false, bouncable = false, allowMove = false;

    public static int potionCount = 0, potionMax = 100, keyCount = 0;

    public Slime() : base(6, 1, ImmuneOn, DeathHandler) {
        instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Playing:
            case GameGlobalController.GameState.Lobby:
                // Control immuable
                //if (immuableTime <= 0) spriteRender.color = new Color(255, 255, 255, 90);
                // Control camera postion, except for the time in the welcome screen
                if (!(GameGlobalController.currentLevel == 0 && GameGlobalController.isLobby))
                    MainCameraHandler.targetPosition = new Vector3(transform.position.x, transform.position.y, -10);
                // Set Slime to follow physics engine
                rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
                // Response to keyboard input
                if (bouncable && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
                {
                    MainCameraHandler.allSound = 2;
                    rigidbody2d.AddForce(new Vector2(150 * moveSpeed, jumpStrength));
                    animator.Play("Jump Right");
                    direction = 1;
                    allowMove = bouncable = false;
                }
                if (bouncable && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
                {
                    MainCameraHandler.allSound = 2;
                    rigidbody2d.AddForce(new Vector2(150 * -moveSpeed, jumpStrength));
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
                    Instantiate(Bomb, pos, transform.rotation).GetComponent<Bullet>().moveSpeed *= direction;
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (potionCount > 0)
                    {
                        Heal(2);
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
        animator.Play("Disappear");
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.collider.tag)
        {
            case "Mushroom":
                Suffer(col.collider.GetComponent<Attackable>().AttackDamage);
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
            case "EventTrigger":
                Animation.handler.trigger(col.GetComponent<TriggerHandler>().triggerId);
                Destroy(col.gameObject);
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Potion":
                MainCameraHandler.allSound = 9;
                Destroy(collision.gameObject);
                potionicon.getP();
                potionCount++;
                break;
             case "Key":
                MainCameraHandler.allSound = 5;
                Destroy(collision.gameObject);
                Keyicon.getK();
                keyCount++;
                break;
        }
    }


    static void ImmuneOn(Entity entity)
    {
        SlimeLifeCanvas.Shake();
        SlimeLifeCanvas.life = entity.health;
        animator.Play("Suffer");
    }

    static void DeathHandler(Entity entity)
    {
        transform.position = new Vector3(-5, -5, -10);
        GameGlobalController.BadEnd();
    }
}
