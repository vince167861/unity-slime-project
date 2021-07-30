#pragma warning disable CS0108
using UnityEngine;

public class Slime : MonoBehaviour//Entity
{
	public int direction;
	public static Slime instance;
	public static Animator animator;
	public static Rigidbody2D rigidbody2d;
	public static SpriteRenderer spriteRender;
	public static Transform transform;


	public GameObject Bomb;
	float moveSpeed = 1600f, jumpStrength = 2e4f, dropStrength = 100f;
	public static float suppression = 1;

	public static bool isTouchingGround = false, bouncable = false, allowMove = false;

	public static int potionCount = 0, potionMax = 100, keyCount = 0;

	public GameObject keyCountObject, potionCountObject, paralysis, heal;

	public Behaviour flareLayer;

	/*public Slime() : base(6, 1, ImmuneOn, DeathHandler) {
        instance = this;
    }*/

	void Start()
	{
		keyCountObject = GameObject.Find("Key Count");
		potionCountObject = GameObject.Find("Potion Count");
		animator = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();
		spriteRender = GetComponent<SpriteRenderer>();
		transform = GetComponent<Transform>();
		flareLayer = Camera.main.GetComponent<FlareLayer>();
		transform.position = new Vector3(-23, -7, 0);
	}

	void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.Loading:
				flareLayer.enabled = false;
				spriteRender.sortingLayerName = "Black Screen";
				spriteRender.sortingOrder = 3;
				break;
			case Game.GameState.DarkFadeIn:
				flareLayer.enabled = true;
				spriteRender.sortingLayerName = "Main Objects";
				spriteRender.sortingOrder = 8;
				break;
			case Game.GameState.Playing:
			case Game.GameState.Lobby:
				moveSpeed = 160 * suppression;
				jumpStrength = 2e4f * suppression;
				dropStrength = 100 * suppression;
				if (LifeHandler.targetlife <= 0 && !LifeHandler.start) DeathHandler();
				// Control immuable
				//if (immuableTime <= 0) spriteRender.color = new Color(255, 255, 255, 90);
				// Control camera postion, except for the time in the welcome screen
				if (!(Game.currentLevel == 0 && Game.isLobby))
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
					animator.SetBool("jump", Input.GetKey(KeyCode.W));
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
				if (Input.GetKeyDown(KeyCode.W) && isTouchingGround)
				{
					MainCameraHandler.allSound = 2;
					rigidbody2d.AddForce(new Vector2(0, jumpStrength));
				}
				if (Input.GetKeyDown(KeyCode.F) && Game.gameState == Game.GameState.Playing)
				{
					if(EnergyHandler.nextenergy < 25 || EnergyHandler.targetenergy < 25)
						MainCameraHandler.allSound = 12;
					else
					{	
						MainCameraHandler.allSound = 4;
						Vector3 pos = transform.position + new Vector3(direction * 5, 0, 0);
						Instantiate(Bomb, pos, transform.rotation).GetComponent<Bullet>().moveSpeed *= direction;
						EnergyHandler.changeamount(-25);
					}
				}
				if (Input.GetKeyDown(KeyCode.Q))
				{
					if (potionCount > 0)
					{
						Instantiate(heal).GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y - 2.5f, transform.position.z);
						LifeHandler.Heal(30);
						potionCountObject.GetComponent<CountLabel>().UpdateCount(--potionCount);
					}
				}
				if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
					allowMove = true;
				break;
			case Game.GameState.Pause:
				rigidbody2d.bodyType = RigidbodyType2D.Static;
				break;
		}
	}
	public static void disappear()
	{
		animator.Play("Disappear");
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.collider.tag)
		{
			case "Mushroom":
				if (Game.gameState == Game.GameState.Playing)
				{
					Instantiate(paralysis).GetComponent<Transform>().position = collision.transform.position;
					LifeHandler.Suffer(collision.collider.GetComponent<Attackable>().AttackDamage);
					GameObject.Find("CharacterLife").GetComponent<Animator>().Play("paralysis");
					Destroy(collision.gameObject);
				}
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

	void OnTriggerStay2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "EventTrigger":
				Animation.handler.trigger(collision.GetComponent<TriggerHandler>().triggerId);
				Destroy(collision.gameObject);
				break;
			case "Potion":
				MainCameraHandler.allSound = 9;
				Destroy(collision.gameObject);
				potionCountObject.GetComponent<CountLabel>().UpdateCount(++potionCount);
				break;
			case "Key":
				MainCameraHandler.allSound = 5;
				Destroy(collision.gameObject);
				keyCountObject.GetComponent<CountLabel>().UpdateCount(++keyCount);
				break;
			case "Hint":
				Game.Hint++;
				if(Game.gameState == Game.GameState.Animation && Game.Hint < LevelVarity.playoval[Game.currentLevel].Count)
					collision.gameObject.GetComponent<Transform>().position = LevelVarity.playoval[Game.currentLevel][Game.Hint];
				if(Game.gameState == Game.GameState.Playing && Game.Hint < LevelVarity.playoval[Game.currentLevel].Count)
					collision.gameObject.GetComponent<Transform>().position = LevelVarity.playoval[Game.currentLevel][Game.Hint];
				if(Game.gameState == Game.GameState.Lobby && Game.Hint < LevelVarity.lobbyoval[Game.currentLevel-1].Count)
					collision.gameObject.GetComponent<Transform>().position = LevelVarity.lobbyoval[Game.currentLevel-1][Game.Hint];
				break;
		}
	}

	public static void Healanim()
	{
		animator.Play("Heal");
	}

	public static void ImmuneOn()//static void ImmuneOn(Entity entity)
	{
		//SlimeLifeCanvas.Shake();
		//SlimeLifeCanvas.life = entity.health;
		animator.Play("Suffer");
	}

	static void DeathHandler()//static void DeathHandler(Entity entity)
	{
		transform.position = new Vector3(-5, -5, -10);
		Game.OnLevelFail();
	}

	/// <summary> For animation 'Start Jump' callback. </summary>
	void Start2()
	{
		animator.Play("Disappear");
		ScreenCover.animator.speed = 1;
	}

	/// <summary> For Game Start animation. </summary>
	void JumpRight()
	{
		MainCameraHandler.allSound = 2;
		rigidbody2d.AddForce(new Vector2(1e4f, 1e4f));
		animator.Play("Jump Right");
		direction = 1;
	}

	/// <summary> For Game Start animation. </summary>
	void SmallJumpRight()
	{
		rigidbody2d.AddForce(new Vector2(4000f, 0.5e4f));
		animator.Play("Jump Right");
		direction = 1;
	}

	void moveback()
	{
		rigidbody2d.AddForce(new Vector2(-6000f, 0));
		animator.Play("Right");
		direction = 1;
	}

	public static void ResetState()
	{
		animator.SetBool("right", false);
		animator.SetBool("left", false);
		animator.SetBool("jump", false);
		animator.SetBool("crouch", false);
	}

	void storyloadend()
	{
		if(Game.storystate == 2)
		{
			animator.Play("Disappear");
			Game.storystate = 3;
			ScreenCover.animator.SetFloat("speed", 1);
		}
	}
}
