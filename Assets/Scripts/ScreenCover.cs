using UnityEngine;

public class ScreenCover : MonoBehaviour
{
	public GameObject dragonPrefab, housePrefab, startScene;
	public static Animator animator;
	public SpriteRenderer spriteRenderer;
#warning Please specify where and when the field 'start' would be used.
	public static bool start = false;
	static GameObject loadingScreen, background2;
	public static bool hadAnimationStarted = false;
	public GameObject loadingScreenReference, background2Reference;
	public Sprite[] Image;
	public Behaviour flareLayer;

	void Start()
	{
		loadingScreen = loadingScreenReference;
		background2 = background2Reference;
		animator = GetComponent<Animator>();
		flareLayer = Camera.main.GetComponent<FlareLayer>();
	}

	private void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.StartGame:
				animator.Play("Start Game");
				break;
			case Game.GameState.Loading:
				// Navigate to different gameState from here.
				switch (Game.storyState)
				{
					case Game.StoryState.NoStory:
						if (Game.currentLevel == 0) Game.gameState = Game.GameState.DarkFadeIn;
						else if (start)
						{
							Slime.ResetState();
							loadingScreen.SetActive(true);
							animator.Play(Game.battle ? "loadgame" : "loadlobby");
							start = false;
						}
						break;
					case Game.StoryState.StartStory:
						Slime.ResetState();
						loadingScreen.SetActive(true);
						animator.Play("loadstory");
						Game.storyState = Game.StoryState.Loading;
						break;
					case Game.StoryState.StoryDragon:
						loadingScreen.SetActive(false);
						Game.SetState("StartStory");
						animator.speed = 1;
						break;
				}
				break;
			case Game.GameState.StartStory:
				MainCameraHandler.storymusic = 1;
				switch (Game.storyState)
				{
					case Game.StoryState.House:
					case Game.StoryState.State7:
						animator.speed = 1;
						break;
				}
				break;
			case Game.GameState.DarkFadeOut:
				if (!hadAnimationStarted)
				{
					spriteRenderer.color = new Color(0, 0, 0);
					animator.Play("Fade Out");
                    hadAnimationStarted = true;
				}
				break;
			case Game.GameState.DarkFadeIn:
				if (!hadAnimationStarted)
				{
					spriteRenderer.color = new Color(0, 0, 0);
					animator.Play("Fade In");
                    hadAnimationStarted = true;
				}
				break;
			case Game.GameState.BrightFadeOut:
				if (!hadAnimationStarted)
				{
					spriteRenderer.color = new Color(1, 1, 1);
					animator.Play("Fade Out");
                    hadAnimationStarted = true;
				}
				break;
			case Game.GameState.BrightFadeIn:
				if (!hadAnimationStarted)
				{
					spriteRenderer.color = new Color(1, 1, 1);
					animator.Play("Fade In");
                    hadAnimationStarted = true;
				}
				break;
		}
	}

	/// <summary> For animation 'Start Game' callback. </summary>
	void Start1()
	{
		animator.speed = 0;
		// Slime.transform.position = new Vector3(-3, 11, 0);
		Slime.instance.animator.Play("Start Jump");
	}

	// Start2() -> Slime.Start2()

	/// <summary> For animation 'Start Game' callback. </summary>
	void Start3()
	{
		startScene.SetActive(false);
		Game.gameState = Game.GameState.MenuPrepare;
	}

	/// <summary> Close loading screen. </summary>
	/// <remark> Also change gameState to DarkFadeIn. </remark>
	void EndLoading()
	{
		loadingScreen.SetActive(false);
		Game.gameState = Game.GameState.DarkFadeIn;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void Story1()
	{
		animator.SetFloat("speed", 0); //animator.speed = 0;
		Game.storyEffect = Game.StoryEffect.ThunderStorm;
		background2.SetActive(true);
		background2.GetComponent<SpriteRenderer>().sprite = Image[0];
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void Story2()
	{
		animator.speed = 0;
		Instantiate(dragonPrefab);
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void clear()
	{
		flareLayer.enabled = false;
		Game.storyEffect = Game.StoryEffect.Clear;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void house()
	{
		Instantiate(housePrefab);
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void story3()
	{
		Game.storyState = Game.StoryState.Light;
		animator.speed = 0;
		flareLayer.enabled = true;
		Game.storyEffect = Game.StoryEffect.Light;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void story4()
	{
		Destroy(GameObject.Find("房子內部(Clone)"));
		Game.storyEffect = Game.StoryEffect.GroundOfFire;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void story5()
	{
		Game.storyState = Game.StoryState.State8;
		animator.speed = 0;
		Game.storychat = 4;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void story6()
	{
		animator.speed = 0;
		Game.storychat = 5;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void story7()
	{
		animator.speed = 0;
		Slime.instance.transform.position = new Vector3(30, 48, 0);
		Game.storychat = 6;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void bubble()
	{
		Game.storyEffect = Game.StoryEffect.Effect4;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void end()
	{
		Game.storyEffect = Game.StoryEffect.Clear;
		Game.storyState = Game.StoryState.State9;
		background2.SetActive(false);
	}

	public static void SkipStory()
	{
		background2.SetActive(false);
		animator.speed = 1;
	}
}
