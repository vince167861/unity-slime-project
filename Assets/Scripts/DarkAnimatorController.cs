using UnityEngine;

public class DarkAnimatorController : MonoBehaviour
{
	public GameObject slimePrefab;
	public static Animator animator;
	SpriteRenderer spriteRenderer;
	public static bool start = true;
	GameObject loading;

	void Start()
	{
		loading = GameObject.Find("Loading...");
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		switch (GameGlobalController.gameState)
		{
			case GameGlobalController.GameState.StartGame:
				if (start)
				{
					loading.SetActive(false);
					animator.Play("startgame");
					start = false;
				}
				break;
			case GameGlobalController.GameState.Loading:
				if (GameGlobalController.currentLevel == 0) loadIn();
				else if (start)
				{
					start = false;
					Slime.normal();
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
				}
				break;
			case GameGlobalController.GameState.fadeOut:
				spriteRenderer.color = Color.HSVToRGB(0, 0, 0);
				animator.Play("black");
				break;
			case GameGlobalController.GameState.fadeIn:
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
		GameGlobalController.gameState = GameGlobalController.GameState.lobbyPrepare;
		GameObject.Find("StartScene").SetActive(false);
	}

	void loadIn()
	{
		loading.SetActive(false);
		GameGlobalController.gameState = GameGlobalController.GameState.fadeIn;
	}
}
