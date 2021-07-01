using UnityEngine;

public class DarkAnimatorController : MonoBehaviour
{
	public GameObject slimePrefab, dragonPrefab;
	public static Animator animator;
	SpriteRenderer spriteRenderer;
	public static bool start = true;
	GameObject loading, background;
	public Sprite[] Image;

	void Start()
	{
		loading = GameObject.Find("Loading...");
		background = GameObject.Find("Background2");
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		switch (GameGlobalController.gameState)
		{
			case GameGlobalController.GameState.StartGame:
				if(start)
				{
					background.SetActive(false);
					loading.SetActive(false);
					animator.Play("startgame");
					start = false;
				}
				break;
			case GameGlobalController.GameState.Loading:
				if(GameGlobalController.storystate == 1)
				{
					Slime.normal();
					loading.SetActive(true);
					Slime.animator.Play("load1");
					Slime.transform.position = new Vector3(46, 14, 0);
					GameGlobalController.storystate = 2;
				}
				else if(GameGlobalController.storystate == 3)
				{
					loading.SetActive(false);
					GameGlobalController.gameState = GameGlobalController.GameState.StartStory;
					animator.speed = 1;
				}
				else if(GameGlobalController.currentLevel == 0 && GameGlobalController.storystate != 2)  loadIn();
				else if(start && GameGlobalController.storystate != 2)
				{
					Slime.normal();
					loading.SetActive(true);
					if(GameGlobalController.battle)
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
			case GameGlobalController.GameState.Darking:
				spriteRenderer.color = Color.HSVToRGB(0, 0, 0);
				animator.Play("black");
				break;
			case GameGlobalController.GameState.Brightening:
				spriteRenderer.color = Color.HSVToRGB(0, 0, 0);
				animator.Play("light");
				break;
			case GameGlobalController.GameState.Lighting:
				spriteRenderer.color = Color.HSVToRGB(0, 0, 100);
				animator.Play("black");
				break;
			case GameGlobalController.GameState.Unlighting:
				spriteRenderer.color = Color.HSVToRGB(0, 0, 100);
				animator.Play("light");
				break;
		}
	}

	void start1()
	{
		animator.speed = 0;
		Slime.transform.position = new Vector3(-3, 11, 0);
		Slime.animator.Play("startjump");
	}

	void start3()
	{
		GameGlobalController.gameState = GameGlobalController.GameState.MenuPrepare;
		GameObject.Find("StartScene").SetActive(false);
	}

	void loadIn()
	{
		loading.SetActive(false);
		GameGlobalController.gameState = GameGlobalController.GameState.Brightening;
	}

	void story1()
	{
		animator.speed = 0;
		GameGlobalController.storyeffect = 1;
		background.SetActive(true);
		background.GetComponent<SpriteRenderer>().sprite = Image[0];
	}

	void story2()
	{
		animator.speed = 0;
		Instantiate(dragonPrefab).GetComponent<Transform>().position = new Vector3();
	}
}
