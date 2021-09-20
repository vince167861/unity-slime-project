#pragma warning disable CS0108
using UnityEngine;

public class Slime : Entity
{
	public int direction;
	public static Slime instance;
	public Animator animator;
	public Rigidbody2D rigidbody2d;
	public SpriteRenderer spriteRenderer;
	public Transform transform;


	public GameObject Bomb;
	public static float suppression = 1;

	public static bool isTouchingGround = false, bouncable = false, allowMove = false;
	public static bool attack, right, left, up, down, healing, go, talk, pose, map = false;

	public static int potionCount = 0, potionMax = 100, keyCount = 0;

	public GameObject keyCountObject, potionCountObject, paralysis, heal;

	static Behaviour flareLayer;

	static readonly Vector3 moveBase = new Vector3(400, 0, 0), jumpBase = new Vector3(0, 18000, 0), dropBase = new Vector3(0, -100, 0);

	public Slime() : base("", 100, 1, SufferCallback, DeathHandler, HealCallback, EffectCallback) {
		instance = this;
	}

	private void Start()
	{
		flareLayer = Camera.main.GetComponent<FlareLayer>();
		transform.position = new Vector3(-23, -7, 0);
		keyCountObject = GameObject.Find("Key Count");
		potionCountObject = GameObject.Find("Potion Count");
	}

	void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.DarkFadeIn:
				flareLayer.enabled = true;
				spriteRenderer.sortingLayerName = "Main Objects";
				spriteRenderer.sortingOrder = 8;
				break;
			case Game.GameState.Playing:
			case Game.GameState.Lobby:
				if(!bouncable && !isTouchingGround)  up = false;
				if(Input.GetKey(KeyCode.X))
				{
					Debug.Log(right);
					Debug.Log(left);
					Debug.Log(down);
				}
				if(pose)
				{
					if(direction == 1)  animator.Play("Jump Right");
					if(direction == -1)	animator.Play("Jump Left");
					pose = false;
				}
				if (health <= 0) DeathHandler(this);
				// Control camera postion, except for the time in the welcome screen
				if (!(Game.currentLevel == 0 && Game.isLobby))
					MainCameraHandler.targetPosition = new Vector3(transform.position.x, transform.position.y, -10);
				// Set Slime to respect physics engine
				rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
				// Response to keyboard input
				if (bouncable && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || left) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || up))
				{
					up = false;
					MainCameraHandler.PlayEntityClip(2);
					rigidbody2d.AddForce((50 * moveBase + 1.5f * jumpBase) * suppression);
					animator.Play("Jump Right");
					direction = 1;
					allowMove = bouncable = false;
				}
				if (bouncable && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || right) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || up))
				{
					up = false;
					MainCameraHandler.PlayEntityClip(2);
					rigidbody2d.AddForce((-50 * moveBase + 1.5f * jumpBase) * suppression);
					animator.Play("Jump Left");
					direction = -1;
					allowMove = bouncable = false;
				}
				if (allowMove)
				{
					animator.SetBool("right", Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || right);
					animator.SetBool("left", Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || left);
					animator.SetBool("jump", Input.GetKey(KeyCode.W) || up);
					animator.SetBool("crouch", Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) || down);
				}
				if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || left) && allowMove)
				{
					rigidbody2d.AddForce(-1 * (isTouchingGround ? 1 : 0.5f) * suppression * moveBase);
					direction = -1;
				}
				if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || right) && allowMove)
				{
					rigidbody2d.AddForce((isTouchingGround ? 1 : 0.5f) * suppression * moveBase);
					direction = 1;
				}
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) || down)
				{
					rigidbody2d.AddForce(dropBase * suppression);
				}
				if ((Input.GetKeyDown(KeyCode.W) || up) && isTouchingGround)
				{
					up = false;
					animator.Play("Jump");
					MainCameraHandler.PlayEntityClip(2);
					rigidbody2d.AddForce(jumpBase * suppression);
				}
				if ((Input.GetKeyDown(KeyCode.F) || attack) && Game.gameState == Game.GameState.Playing)
				{
					attack = false;
					if(EnergyHandler.nextenergy < 25 || EnergyHandler.targetenergy < 25)
						MainCameraHandler.PlayEntityClip(12);
					else
					{
						MainCameraHandler.PlayEntityClip(4);
						Vector3 pos = transform.position + new Vector3(direction * 5, 0, 0);
						Instantiate(Bomb, pos, transform.rotation).GetComponent<Bullet>().moveSpeed *= direction;
						EnergyHandler.changeamount(-25);
					}
				}
				if (Input.GetKeyDown(KeyCode.Q) || healing)
				{
					healing = false;
					if (potionCount > 0)
					{
						Instantiate(heal, transform.position + new Vector3(0, -2.5f, 0), Quaternion.identity);
						Heal(30);
						potionCountObject.GetComponent<CountLabel>().UpdateCount(--potionCount);
					}
				}
				if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || left || right)
					allowMove = true;
				break;
			case Game.GameState.Pause:
				rigidbody2d.bodyType = RigidbodyType2D.Static;
				break;
		}
	}

	public static void PreLoading()
	{
		instance.rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
		flareLayer.enabled = false;
		instance.spriteRenderer.sortingLayerName = "Black Screen";
		instance.spriteRenderer.sortingOrder = 3;
		switch (Game.storyState)
		{
			case Game.StoryState.NoStory:
				if (Game.currentLevel != 0)
				{
					instance.transform.position = new Vector3(8, -3, 0);
					instance.animator.Play(Game.battle ? "load1" : "load2");
				}
				break;
			case Game.StoryState.StartStory:
				instance.transform.position = new Vector3(8, -2, 0);
				instance.animator.Play("load1");
				break;
			case Game.StoryState.StoryDragon:
				instance.transform.position = new Vector3(-5, -5, 0);
				break;
		}
	}

	public void disappear()
	{
		animator.Play("Disappear");
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.collider.tag)
		{
			case "Ground":
				bouncable = false;
				break;
			case "Walls":
				if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || left || right)
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
				MainCameraHandler.PlayEntityClip(9);
				Destroy(collision.gameObject);
				potionCountObject.GetComponent<CountLabel>().UpdateCount(++potionCount);
				break;
			case "Key":
				MainCameraHandler.PlayEntityClip(5);
				Destroy(collision.gameObject);
				keyCountObject.GetComponent<CountLabel>().UpdateCount(++keyCount);
				break;
			case "Hint":
				Game.Hint++;
				if(Game.gameState == Game.GameState.Dialog && Game.Hint < DataStorage.playoval[Game.currentLevel].Count)
					collision.gameObject.GetComponent<Transform>().position = DataStorage.playoval[Game.currentLevel][Game.Hint];
				if(Game.gameState == Game.GameState.Playing && Game.Hint < DataStorage.playoval[Game.currentLevel].Count)
					collision.gameObject.GetComponent<Transform>().position = DataStorage.playoval[Game.currentLevel][Game.Hint];
				if(Game.gameState == Game.GameState.Lobby && Game.Hint < DataStorage.lobbyoval[Game.currentLevel-1].Count)
					collision.gameObject.GetComponent<Transform>().position = DataStorage.lobbyoval[Game.currentLevel-1][Game.Hint];
				break;
		}
	}

	public static void HealCallback(Entity entity, float amount)
	{
		instance.animator.Play("Heal");
		MainCharacterHealth.Heal(amount);
	}

	public static void SufferCallback(Entity entity, float damage)
	{
		instance.animator.Play("Suffer");
		MainCharacterHealth.Suffer(damage);
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
		MainCameraHandler.PlayEntityClip(2);
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
		rigidbody2d.AddForce(new Vector2(-4.5e3f, 0));
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

	void StoryLoadEnd()
	{
		if (Game.storyState == Game.StoryState.Loading)
		{
			instance.transform.position = new Vector3(-50, -50, 0);
			animator.Play("Disappear");
			Game.storyState = Game.StoryState.StoryDragon;
			ScreenCover.PreLoading();
		}
	}

	static void EffectCallback(Entity entity, EntityEffect effect)
	{
		switch (effect.effectType)
		{
			case EntityEffect.EntityEffectType.Paralyze:
				GameObject.Find("Character Status").GetComponent<Animator>().Play("paralysis");
				break;
		}
	}
}
