#pragma warning disable CS0108
using UnityEngine;

public class Slime : Entity
{
	public int direction;
	public static Slime instance;
	public Animator animator;
	public Rigidbody2D rigidbody2d;
	public SpriteRenderer spriteRender;
	public Transform transform;


	public GameObject Bomb;
	public static float suppression = 1;

	public static bool isTouchingGround = false, bouncable = false, allowMove = false;

	public static int potionCount = 0, potionMax = 100, keyCount = 0;

	public GameObject keyCountObject, potionCountObject, paralysis, heal;

	public Behaviour flareLayer;

	public static readonly Vector3 moveBase = new Vector3(1600, 0, 0), jumpBase = new Vector3(0, 2e4f, 0), dropBase = new Vector3(0, -100, 0);

	public Slime() : base("", 6, 1, ImmuneOn, DeathHandler) {
      instance = this;
  }

	private void Start()
	{
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
				switch (Game.storyState)
				{
					case Game.StoryState.NoStory:
						if (Game.currentLevel != 0)
						{
							transform.position = new Vector3(8, -5, 0);
							animator.Play(Game.battle ? "load1" : "load2");
						}
						break;
					case Game.StoryState.StartStory:
						transform.position = new Vector3(8, -5, 0);
						animator.Play("load1");
						break;
					case Game.StoryState.StoryDragon:
						transform.position = new Vector3(-5, -5, 0);
						break;
				}
				break;
			case Game.GameState.DarkFadeIn:
				flareLayer.enabled = true;
				spriteRender.sortingLayerName = "Main Objects";
				spriteRender.sortingOrder = 8;
				break;
			case Game.GameState.Playing:
			case Game.GameState.Lobby:
				if (health <= 0) DeathHandler(this);
				// Control camera postion, except for the time in the welcome screen
				if (!(Game.currentLevel == 0 && Game.isLobby))
					MainCameraHandler.targetPosition = new Vector3(transform.position.x, transform.position.y, -10);
				// Set Slime to respect physics engine
				rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
				// Response to keyboard input
				if (bouncable && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
				{
					MainCameraHandler.allSound = 2;
					rigidbody2d.AddForce((150 * moveBase + jumpBase) * suppression);
					animator.Play("Jump Right");
					direction = 1;
					allowMove = bouncable = false;
				}
				if (bouncable && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
				{
					MainCameraHandler.allSound = 2;
					rigidbody2d.AddForce((-150 * moveBase + jumpBase) * suppression);
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
					rigidbody2d.AddForce(-1 * (isTouchingGround ? 1 : 0.5f) * suppression * moveBase);
					direction = -1;
				}
				if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && allowMove)
				{
					rigidbody2d.AddForce((isTouchingGround ? 1 : 0.5f) * suppression * moveBase);
					direction = 1;
				}
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S))
					rigidbody2d.AddForce(dropBase * suppression);
				if (Input.GetKeyDown(KeyCode.W) && isTouchingGround)
				{
					MainCameraHandler.allSound = 2;
					rigidbody2d.AddForce(jumpBase * suppression);
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
						Heal(30);
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
	public void disappear()
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
					Suffer(collision.collider.GetComponent<IAttackable>().AttackDamage);
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

	public void Healanim()
	{
		animator.Play("Heal");
	}

	public static void ImmuneOn(Entity entity)
	{
		instance.animator.Play("Suffer");
	}

	static void DeathHandler(Entity entity)
	{
		instance.transform.position = new Vector3(-5, -5, -10);
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
		rigidbody2d.AddForce(new Vector2(7.5e3f, 7.5e3f));
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
		instance.animator.SetBool("right", false);
		instance.animator.SetBool("left", false);
		instance.animator.SetBool("jump", false);
		instance.animator.SetBool("crouch", false);
	}

	void storyloadend()
	{
		if (Game.storyState == Game.StoryState.Loading)
		{
			animator.Play("Disappear");
			Game.storyState = Game.StoryState.StoryDragon;
			ScreenCover.animator.SetFloat("speed", 1);
		}
	}
}
