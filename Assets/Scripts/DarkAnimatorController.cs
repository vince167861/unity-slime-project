using UnityEngine;

public class DarkAnimatorController : MonoBehaviour
{
	public GameObject slimePrefab, dragonPrefab, housePrefab, startScene;
	public static Animator animator;
	private SpriteRenderer spriteRenderer;
#warning Please specify where and when the field 'start' would be used.
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
		switch (Game.gameState)
		{
			case Game.GameState.StartGame:
				if (start)
				{
					background.SetActive(false);
					loading.SetActive(false);
					animator.Play("Start Game");
					start = false;
				}
				break;
			case Game.GameState.Loading:
				switch (Game.storystate)
				{
					case 0:
						if (Game.currentLevel == 0) EndLoading();
						else if (start)
						{
							Slime.ResetState();
							loading.SetActive(true);
							if (Game.battle)
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
						Game.storystate = 2;
						break;
					case 3:
						Slime.transform.position = new Vector3(-5, -5, 0);
						loading.SetActive(false);
						Game.gameState = Game.GameState.StartStory;
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
		Slime.transform.position = new Vector3(-3, 11, 0);
		Slime.animator.Play("Start Jump");
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
		loading.SetActive(false);
		Game.SetState("DarkFadeIn");
	}

	/// <summary> For animation 'Start Story' callback. </summary>
	void Story1()
	{
		animator.SetFloat("speed", 0);
		//animator.speed = 0;
		Game.storyeffect = 1;
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
		Slime.transform.position = new Vector3(30, 48, 0);
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
		background.SetActive(false);
	}

	public static void SkipStory()
	{
		background.SetActive(false);
		animator.speed = 1;
	}
}
