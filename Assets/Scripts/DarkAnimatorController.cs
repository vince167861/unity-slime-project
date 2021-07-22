using UnityEngine;

public class DarkAnimatorController : MonoBehaviour
{
	public GameObject slimePrefab, dragonPrefab, housePrefab, startScene;
	public static Animator animator;
	SpriteRenderer spriteRenderer;
	public static bool start = true;
	public static GameObject loading, background;
	public Sprite[] Image;
	public Behaviour flareLayer;

	void Start()
	{
		loading = GameObject.Find("Loading...");
		background = GameObject.Find("Background2");
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		flareLayer = (Behaviour)Camera.main.GetComponent ("FlareLayer");
	}

	private void Update()
	{
		switch (GameGlobalController.gameState)
		{
			case GameGlobalController.GameState.StartGame:
				if (start)
				{
					background.SetActive(false);
					loading.SetActive(false);
					animator.Play("Start Game");
					start = false;
				}
				break;
			case GameGlobalController.GameState.Loading:
				switch (GameGlobalController.storystate)
				{
					case 0:
						if (GameGlobalController.currentLevel == 0) EndLoading();
						else if (start)
						{
							Slime.ResetState();
							loading.SetActive(true);
							if (GameGlobalController.battle)
							{
								Slime.animator.Play("load1");
								animator.Play("loadgame");
							}
							else
							{
								Slime.animator.Play("load2");
								animator.Play("loadlobby");
							}
							Slime.transform.position = new Vector3(46, 14, 0);
							start = false;
						}
						break;
					case 1:
						Slime.ResetState();
						loading.SetActive(true);
						Slime.animator.Play("load1");
						animator.Play("loadstory");
						Slime.transform.position = new Vector3(43, 14, 0);
						GameGlobalController.storystate = 2;
						break;
					case 3:
						Slime.transform.position = new Vector3(-5, -5, 0);
						loading.SetActive(false);
						GameGlobalController.gameState = GameGlobalController.GameState.StartStory;
						animator.speed = 1;
						break;
				}
				break;
			case GameGlobalController.GameState.StartStory:
				MainCameraHandler.storymusic = 1;
				switch (GameGlobalController.storystate)
				{
					case 5:
					case 7:
						animator.speed = 1;
						break;
				}
				break;
			case GameGlobalController.GameState.DarkFadeOut:
				spriteRenderer.color = new Color(0, 0, 0);
				animator.Play("black");
				break;
			case GameGlobalController.GameState.DarkFadeIn:
				spriteRenderer.color = new Color(0, 0, 0);
				animator.Play("Dark Fade In");
				break;
			case GameGlobalController.GameState.BrightFadeOut:
				spriteRenderer.color = new Color(1, 1, 1);
				animator.Play("black");
				break;
			case GameGlobalController.GameState.BrightFadeIn:
				spriteRenderer.color = new Color(1, 1, 1);
				animator.Play("light");
				break;
		}
	}

	/// <summary> For animation 'Start Game' callback. </summary>
	void Start1()
	{
		animator.speed = 0;
		Slime.transform.position = new Vector3(-3, 11, 0);
		Slime.animator.Play("Start Jump");
	}

	// Start2() -> Slime.Start2()

	/// <summary> For animation 'Start Game' callback. </summary>
	void Start3()
	{
		startScene.SetActive(false);
		GameGlobalController.gameState = GameGlobalController.GameState.MenuPrepare;
	}

	/// <summary> Close loading screen. </summary>
	void EndLoading()
	{
		loading.SetActive(false);
		GameGlobalController.gameState = GameGlobalController.GameState.DarkFadeIn;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void Story1()
	{
		animator.SetFloat("speed", 0);
		//animator.speed = 0;
		GameGlobalController.storyeffect = 1;
		background.SetActive(true);
		background.GetComponent<SpriteRenderer>().sprite = Image[0];
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void Story2()
	{
		animator.speed = 0;
		Instantiate(dragonPrefab).GetComponent<Transform>().position = new Vector3(60, 50 ,0);
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void clear()
	{
		flareLayer.enabled = false;
		GameGlobalController.cleareffect = true;
	}

	void house()
	{
		Instantiate(housePrefab);
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void story3()
	{
		GameGlobalController.storystate = 6;
		animator.speed = 0;
		flareLayer.enabled = true;
		GameGlobalController.storyeffect = 2;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void story4()
	{
		Destroy(GameObject.Find("房子內部(Clone)"));
		GameGlobalController.storyeffect = 3;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void story5()
	{
		GameGlobalController.storystate = 8;
		animator.speed = 0;
		GameGlobalController.storychat = 4;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void story6()
	{
		animator.speed = 0;
		GameGlobalController.storychat = 5;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void story7()
	{
		animator.speed = 0;
		Slime.transform.position = new Vector3(30, 48, 0);
		GameGlobalController.storychat = 6;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void bubble()
	{
		GameGlobalController.storyeffect = 4;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void end()
	{
		GameGlobalController.cleareffect = true;
		GameGlobalController.storystate = 9;
		background.SetActive(false);
	}

	public static void SkipStory()
	{
		background.SetActive(false);
		animator.speed = 1;
	}
}
