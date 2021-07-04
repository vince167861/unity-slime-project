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

	public enum GameState { StartGame, StartStory, Input, Start, MenuPrepare, Darking, Loading, Brightening, Playing, Pause, Instruction, End, Lobby, Animation, Shaking, Lighting, Unlighting, LobbyInfo, Advice };
	public static GameState gameState = GameState.StartGame;
	public static int currentLevel = 0;
	public static bool battle = false, isMake = false, cleareffect = false, isStory = false;
	public static int storystate = 0; // 0:unstory 1:startstory 2:loading 3:storydragon 4:dragonshow 5:house 6:light 7:
	public static int storyeffect = 0; // 0:null 1:big_rain 2:light
	public static int storychat = 0; // 0:null 1:??? 2:what? 3:escape 4:rescue 5:turn 6:slime
	float delta = 0;

	public GameObject board, brand, dialogBox, help, pauseButton, potionicon, keyicon, lobbyinfo, turnBack, skip, inputfield;
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
		inputfield.SetActive(storystate == 9 || gameState == GameState.Input);
		skip.SetActive(gameState == GameState.StartStory && storystate >= 3);
		lobbyCanvas.SetActive(isLobby && currentLevel == 0 && !battle);
		passCanvas.SetActive(hasEnded && !battle);
		deadCanvas.SetActive(hasEnded && battle);
		slimeHealthCanvas.SetActive(isPlaying || isAnimation || gameState == GameState.Shaking || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Playing);
		// GameObjects
		brand.SetActive(gameState == GameState.Lobby && currentLevel > 0 || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Lobby);
		dialogBox.SetActive(isAnimation || gameState == GameState.Advice || (gameState == GameState.StartStory && storychat != 0));
		pauseButton.SetActive(!isPaused && gameState != GameState.StartGame && gameState != GameState.StartStory && gameState != GameState.Loading);
		board.SetActive(isPaused);
		help.SetActive(gameState == GameState.Instruction);
		potionicon.SetActive(isPlaying || isAnimation || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Playing);
		keyicon.SetActive(isPlaying || isAnimation || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Playing);
		lobbyinfo.SetActive(gameState == GameState.LobbyInfo);
		turnBack.SetActive(gameState == GameState.LobbyInfo);

		switch (gameState)
		{
			case GameState.Input:
				DarkAnimatorController.skip();
				break;
			case GameState.StartGame:
				break;
			case GameState.StartStory:
				switch(storyeffect)
				{
					case 0:
						break;
					case 1:
						cleareffect = false;
						Instantiate(weather[0]);
						storyeffect = 0;
						gameState = GameState.Loading;
						break;
					case 2:
						cleareffect = false;
						Instantiate(weather[6]);
						storyeffect = 0;
						break;
					case 3:
						cleareffect = false;
						Instantiate(weather[5]);
						storyeffect = 0;
						break;
					case 4:
						cleareffect = false;
						Instantiate(force[1]);
						storyeffect = 0;
						break;
				}
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
			case GameState.Darking:
				delta += Time.deltaTime;
				if (delta >= 1)
				{
					delta = 0;
					if(!battle && currentLevel > 0)
					{
						if(currentLevel == 1)  Instantiate(weather[1]);
						else  Instantiate(weather[Random.Range(1,4)]);
					}
					else if(battle && LevelVarity.LevelWeather[0][currentLevel] != -1)  Instantiate(force[LevelVarity.LevelWeather[0][currentLevel]]);
					if(battle && currentLevel == 1) Instantiate(weather[4]).GetComponent<Transform>().position = new Vector3(-30, 56, 0);
					GameGlobalController.gameState = GameGlobalController.battle ? GameState.Start : GameState.MenuPrepare;
				}
				break;
			case GameState.MenuPrepare:
				guildwoman.startanim = true;
				DarkAnimatorController.start = true;
				gameState = GameState.Loading;
				break;
			case GameState.Start:
				if(LevelVarity.me == null)
					gameState = GameState.Input;
				else
				{
					Slime.keyCount = 0;
					DarkAnimatorController.start = true;
					gameState = GameState.Loading;
				}
				break;
			case GameState.Brightening:
				if(!isMake)
				{
					if(battle)	MakeMap(0);
					else  MakeMap(1);
					isMake = true;
				}
				LifeHandler.start = true;
				background.sprite = battle ? gameBackground[currentLevel] : menuBackground[currentLevel];
				delta += Time.deltaTime;
				if (delta >= 1)
				{
					delta = 0;
					isMake = false;
					if(storystate == 0 && !isStory) 
					{
						Instantiate(weather[5]);
						storystate = 1;
						isStory = true;
					}
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
		gameState = GameState.Darking;
	}

	public static void GotoLobby()
	{
		battle = false;
		gameState = GameState.Darking;
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
	public static bool isMenuPrepare => gameState == GameState.MenuPrepare;
	public static bool isDarking => gameState == GameState.Darking;
	public static bool hasEnded => gameState == GameState.End;

	public static void SetPlaying() { gameState = GameState.Playing; }
	public static void SetAnimation() { gameState = GameState.Animation; }

	public static void givename()
	{
		for(int i = 0;i < LevelVarity.teller.Count;i++)
		{
			for(int j = 0;j < LevelVarity.teller[i].Count;j++)
			{
				if(LevelVarity.teller[i][j] == null)  LevelVarity.teller[i][j] = LevelVarity.me;
			}
		}
	}

}
