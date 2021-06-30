using UnityEngine;

public class GameGlobalController : MonoBehaviour
{
	/// Imports
	// Canvases
	public GameObject lobbyCanvas, passCanvas, deadCanvas, slimeHealthCanvas;
	// Prefabs
	public GameObject slimePrefab, enemySpawnerPrefab, brickPrefab, portalPrefab, floorPrefab, instructPrefab;
	// Level terrians
	public GameObject[] levelPrefab;
	// Backgrounds
	public Sprite[] gameBackground, menuBackground;
	// Effects
	public GameObject[] weather, force;
	public static GameState gameState = GameState.StartGame;
	public enum GameState { StartGame, Loading, LevelPrepare, lobbyPrepare, fadeOut, fadeIn, Playing, Pause, Instruction, End, Lobby, Animation, Shaking, Lighting, Unlighting, LobbyInfo, Advice };
	public static int currentLevel = 0;
	public static bool battle = false, isMake = false;
	float delta = 0;

	public GameObject board, brand, dialogBox, help, pauseButton, potionicon, keyicon, lobbyinfo, turnBack;
	public static GameObject keyCountObject;
	SpriteRenderer background;

	void Start()
	{
		
		keyCountObject = GameObject.Find("Key Count");
		background = GetComponent<SpriteRenderer>();
		Instantiate(slimePrefab);
	}

	void Update()
	{
		/// Show or hide items
		// Canvases
		lobbyCanvas.SetActive(isLobby && currentLevel == 0 && !battle);
		passCanvas.SetActive(hasEnded && !battle);
		deadCanvas.SetActive(hasEnded && battle);
		slimeHealthCanvas.SetActive(isPlaying || isAnimation || gameState == GameState.Shaking || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Playing);
		// GameObjects
		brand.SetActive(gameState == GameState.Lobby && currentLevel > 0 || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Lobby);
		dialogBox.SetActive(isAnimation || gameState == GameState.Advice);
		pauseButton.SetActive(!isPaused && gameState != GameState.StartGame);
		board.SetActive(isPaused);
		help.SetActive(gameState == GameState.Instruction);
		potionicon.SetActive(isPlaying || isAnimation || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Playing);
		keyicon.SetActive(isPlaying || isAnimation || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Playing);
		lobbyinfo.SetActive(gameState == GameState.LobbyInfo);
		turnBack.SetActive(gameState == GameState.LobbyInfo);

		switch (gameState)
		{
			case GameState.StartGame:
				break;
			case GameState.Loading:
				break;
			case GameState.Lighting:
				delta += Time.deltaTime;
				if (delta >= 1)
				{
					delta = 0;
					gameState = GameState.Unlighting;
				}
				break;
			case GameState.Unlighting:
				delta += Time.deltaTime;
				if (delta >= 1)
				{
					delta = 0;
					gameState = battle ? GameState.Playing : GameState.Lobby;
				}
				break;
			case GameState.fadeOut:
				delta += Time.deltaTime;
				if (delta >= 1)
				{
					delta = 0;
					if(!battle && currentLevel > 0)
					{
						if(currentLevel == 1) Instantiate(weather[1]);
						else Instantiate(weather[Random.Range(1,4)]);
					}
					else if(battle && LevelVarity.LevelWeather[0][currentLevel] != -1) Instantiate(force[LevelVarity.LevelWeather[0][currentLevel]]);
					if(currentLevel == 1) Instantiate(weather[4]).GetComponent<Transform>().position = new Vector3(-30, 56, 0);
					background.sprite = battle ? gameBackground[currentLevel] : menuBackground[currentLevel];
					gameState = battle ? GameState.LevelPrepare : GameState.lobbyPrepare;
				}
				break;
			case GameState.lobbyPrepare:
				guildwoman.startanim = true;
				if (currentLevel != 0)
				{
					Slime.transform.position = new Vector2(1f, 5f);
					Instantiate(floorPrefab);
				}
				gameState = GameState.fadeIn;
				DarkAnimatorController.start = true;
				gameState = GameState.Loading;
				break;
			case GameState.LevelPrepare:
				Slime.keyCount = 0;
				DarkAnimatorController.start = true;
				gameState = GameState.Loading;
				break;
			case GameState.fadeIn:
				if (!isMake)
				{
					if(battle)	MakeMap(0);
					else  MakeMap(1);
					isMake = true;
				}
				Slime.transform.position = LevelVarity.spawnpoint[currentLevel];
				Instantiate(levelPrefab[currentLevel]);
				gameState = GameState.fadeIn;
				break;
				LifeHandler.start = true;
				delta += Time.deltaTime;
				if (delta >= 1)
				{
					delta = 0;
					isMake = false;
					gameState = battle ? GameState.Playing : GameState.Lobby;
				}
				break;
			case GameState.Playing:
				break;
			case GameState.Pause:
				break;
			case GameState.End:
				break;
			case GameState.Lobby:
				break;
			case GameState.Animation:
				break;
			case GameState.Shaking:
				break;
			case GameState.Instruction:
				break;
			case GameState.LobbyInfo:
				break;
			case GameState.Advice:
				break;
		}
	}

	public static void StartNewGame()
	{
		battle = true; // Starts the battle
		gameState = GameState.fadeOut;
	}

	public static void GotoLobby()
	{
		battle = false;
		gameState = GameState.fadeOut;
	}

	public static void GoodEnd()
	{
		battle = false; // Ends the battle
		keyCountObject.GetComponent<CountLabel>().updateCount(0);
		gameState = GameState.End;
	}
	public static void BadEnd()
	{
		keyCountObject.GetComponent<CountLabel>().updateCount(0);
		gameState = GameState.End;
	}

	void MakeMap(int which)
	{
		if(which == 0)
		{
			if (currentLevel < LevelVarity.spawnpoint.Count)
			{
				Slime.transform.position = LevelVarity.spawnpoint[currentLevel];
				Instantiate(levelPrefab[currentLevel]);
			}
		}
		else
		{
			if (currentLevel != 0)
			{
				Slime.transform.position = new Vector2(1f, 5f);
				Instantiate(floorPrefab);
			}
		}
	}
	public static bool isPlaying => gameState == GameState.Playing;
	public static bool isPaused => gameState == GameState.Pause;
	public static bool isLobby => gameState == GameState.Lobby;
	public static bool isAnimation => gameState == GameState.Animation;
	public static bool isMenuPrepare => gameState == GameState.lobbyPrepare;
	public static bool isDarking => gameState == GameState.fadeOut;
	public static bool isBrightening => gameState == GameState.fadeIn;
	public static bool hasEnded => gameState == GameState.End;
	public static bool isStart => gameState == GameState.LevelPrepare;

	public static void SetPlaying() { gameState = GameState.Playing; }
	public static void SetAnimation() { gameState = GameState.Animation; }
}
