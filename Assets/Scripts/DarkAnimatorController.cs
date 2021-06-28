using UnityEngine;

public class DarkAnimatorController : MonoBehaviour
{
	public GameObject slimePrefab;
	public static Animator animator;
	SpriteRenderer spriteRenderer;
	public static bool start = true;

	void Start()
	{
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
					GameObject.Find("Loading...").SetActive(false);
					animator.Play("startgame");
					start = false;
				}
				break;
			case GameGlobalController.GameState.Loading:
				if(start)
				{
					GameObject.Find("Loading...").SetActive(true);
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
					Slime.transform.position = new Vector3(-3, 11, 0);
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
		GameGlobalController.gameState = GameGlobalController.GameState.Brightening;
		GameObject.Find("Loading...").SetActive(false);
	}
}
