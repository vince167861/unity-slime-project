using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SlimeHandler : Entity
{
    public SlimeHandler() : base(6) { }

    public GameObject Bomb;
    public int bombdirect = 1;//-1=left,1=right
    float moveSpeed = 1.2e2f; //main character movement speed
    float jumpStrenght = 2e4f; //main character jump strenght
    float dropStrenght = 100f; //main character drop strenght

    Animator anim;
    Transform camera_T;
    Rigidbody2D rg2d;
    Vector2 scrCtrPos;
    SpriteRenderer backgroundSpriteRenderer;
    public bool isTouchingBrick = false;
    public bool allowMove = true;
    public bool isTouchingWall = false;

    float immuneResetTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rg2d = GetComponent<Rigidbody2D>();
        scrCtrPos = new Vector2(Screen.width / 2, Screen.height / 2);
        backgroundSpriteRenderer = GameObject.Find("Background").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Playing:
            case GameGlobalController.GameState.Lobby:
                if (immuneResetTimer <= 0)
                    GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 90);
                else
                    immuneResetTimer -= Time.deltaTime;
                if (isAttacked)
                {
                    immuneResetTimer = 0.2f;
                    GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 90);
                    isAttacked = false;
                }
                MainCameraHandler.targetPosition = new Vector3(transform.position.x, transform.position.y, -10);
                rg2d.bodyType = RigidbodyType2D.Dynamic;
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S))
                {
                    rg2d.AddForce(new Vector2(0, -dropStrenght));
                    anim.Play("slime_crouch");
                }
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isTouchingBrick)
                {
                    MainCameraHandler.allSound=2;
                    rg2d.AddForce(new Vector2(0, jumpStrenght));
                    anim.Play("slime_jump");
                }
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && Input.GetKey(KeyCode.A) && isTouchingWall && allowMove)
                {
                    MainCameraHandler.allSound=2;
                    rg2d.AddForce(new Vector2(100 * moveSpeed, jumpStrenght));
                    anim.Play("slime_jump");
                    allowMove = false;
                }
                else if (Input.GetKey(KeyCode.A) && allowMove)
                {
                    rg2d.AddForce(new Vector2(-moveSpeed, 0));
                    anim.Play("slime_left");
                    bombdirect = -1;
                }
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && Input.GetKey(KeyCode.D) && isTouchingWall && allowMove)
                {
                    MainCameraHandler.allSound=2;
                    rg2d.AddForce(new Vector2(100 * -moveSpeed, jumpStrenght));
                    allowMove = false;
                    anim.Play("slime_jump");
                }
                else if (Input.GetKey(KeyCode.D) && allowMove)
                {
                    rg2d.AddForce(new Vector2(moveSpeed, 0));
                    anim.Play("slime_right");
                    bombdirect = 1;
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Vector3 pos = transform.position;
                    BulletHandler bulletScript = Instantiate(Bomb, pos, transform.rotation).GetComponent<BulletHandler>();
                    bulletScript.moveSpeed *= bombdirect;
                }
                if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                {
                    allowMove = true;
                }
                break;
            case GameGlobalController.GameState.Pause:
                break;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.collider.tag)
        {
            case "Enemy":
                Suffer(col.collider.GetComponent<Attackable>().AttackDamage);
                SlimeLifeCanvas.life = health;
                break;
            case "Floor":
                isTouchingBrick = true;
                break;
            case "Ground":
                isTouchingBrick = true;
                break;
            case "Walls":
                isTouchingWall = true;
                break;
            case "Brick":
                isTouchingBrick = true;
                break;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        switch (col.collider.tag)
        {
            case "Floor":
                isTouchingBrick = false;
                break;
            case "Ground":
                isTouchingBrick = false;
                break;
            case "Walls":
                isTouchingWall = false;
                break;
            case "Brick":
                isTouchingBrick = false;
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Enemy":
                if(GameGlobalController.gameState!=GameGlobalController.GameState.Animation){
                    SlimeLifeCanvas.Shake();
                    Suffer(col.GetComponent<Attackable>().AttackDamage);
                    SlimeLifeCanvas.life = health;
                }
                break;
            case "EventTrigger":
                Animation.handler.trigger(col.GetComponent<TriggerHandler>().triggerId);
                Destroy(col.gameObject);
                break;
        }
    }
}
