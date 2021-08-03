using UnityEngine;

public class ScreenCover : MonoBehaviour
{
	public GameObject dragonPrefab, housePrefab, startScene;
	public static Animator animator;
	public SpriteRenderer spriteRenderer;
#warning Please specify where and when the field 'start' would be used.
	public static bool start = false;
	private static GameObject loadingScreen, background2;
	public GameObject loadingScreenReference, background2Reference;
	public Sprite[] Image;
	public Behaviour flareLayer;

	void Start()
	{
		loadingScreen = loadingScreenReference;
		background2 = background2Reference;
		animator = GetComponent<Animator>();
		flareLayer = (Behaviour)Camera.main.GetComponent ("FlareLayer");
	}

	private void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.StartGame:
				animator.Play("Start Game");
				break;
			case Game.GameState.Loading:
				switch (Game.storystate)
				{
					case 0:
						if (Game.currentLevel == 0) EndLoading();
						else if (start)
						{
							Slime.ResetState();
							loadingScreen.SetActive(true);
							animator.Play(Game.battle ? "loadgame" : "loadlobby");
							start = false;
						}
						break;
					case 1:
						Slime.ResetState();
						loadingScreen.SetActive(true);
						animator.Play("loadstory");
						Game.storystate = 2;
						break;
					case 3:
						loadingScreen.SetActive(false);
						Game.SetState("StartStory");
						animator.speed = 1;
						break;
				}
				break;
			case Game.GameState.StartStory:
				MainCameraHandler.storymusic = 1;
				switch (Game.storystate)
				{
					case 5:
					case 7:
						animator.speed = 1;
						break;
				}
				break;
			case Game.GameState.DarkFadeOut:
				spriteRenderer.color = new Color(0, 0, 0);
				animator.Play("Fade Out");
				break;
			case Game.GameState.DarkFadeIn:
				spriteRenderer.color = new Color(0, 0, 0);
				animator.Play("Fade In");
				break;
			case Game.GameState.BrightFadeOut:
				spriteRenderer.color = new Color(1, 1, 1);
				animator.Play("Fade Out");
				break;
			case Game.GameState.BrightFadeIn:
				spriteRenderer.color = new Color(1, 1, 1);
				animator.Play("Fade In");
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
		Game.SetState("MenuPrepare");
	}

	/// <summary> Close loading screen. </summary>
	void EndLoading()
	{
		loadingScreen.SetActive(false);
		Game.SetState("DarkFadeIn");
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void Story1()
	{
		animator.SetFloat("speed", 0);
		//animator.speed = 0;
		Game.storyeffect = 1;
		background2.SetActive(true);
		background2.GetComponent<SpriteRenderer>().sprite = Image[0];
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
		Game.cleareffect = true;
	}

	void house()
	{
		Instantiate(housePrefab);
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void story3()
	{
		Game.storystate = 6;
		animator.speed = 0;
		flareLayer.enabled = true;
		Game.storyeffect = 2;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void story4()
	{
		Destroy(GameObject.Find("房子內部(Clone)"));
		Game.storyeffect = 3;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void story5()
	{
		Game.storystate = 8;
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
		Game.storyeffect = 4;
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void end()
	{
		Game.cleareffect = true;
		Game.storystate = 9;
		background2.SetActive(false);
	}

	public static void SkipStory()
	{
		background2.SetActive(false);
		animator.speed = 1;
	}
}
