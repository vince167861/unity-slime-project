using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	/// Imports
	// Canvases
	public GameObject lobbyCanvas, passCanvas, deadCanvas, slimeHealthCanvas, mapCanvas;
	// Prefabs
	public GameObject slimePrefab, enemySpawnerPrefab, brickPrefab, portalPrefab, floorPrefab, instructPrefab;
	// Level terrians
	public GameObject[] levelPrefab;
	// Backgrounds
	public Sprite[] gameBackground, menuBackground;
	// Effects
	public GameObject[] weather, force;

	/// <summary> Used to describe a game state. </summary>
	/// <remarks> Add xml comment when add new state. </remarks>
	public enum GameState
	{
		StartGame, StartStory, Input,
		/// <summary> State before every level starts. </summary>
		/// <remarks> Was 'Start' before. </remarks>
		LevelPrepare,
		/// <summary> State before enter menu. </summary>
		MenuPrepare,
		/// <summary> Fade out to dark color. </summary>
		/// <remarks> Was 'Darking' before.</remarks>
		DarkFadeOut, Loading,
		/// <summary> Fade in from dark color. </summary>
		/// <remarks> Was 'Brightening' before. </remarks>
		DarkFadeIn,
		/// <summary> State when players are playing in every level. </summary>
		Playing,
		/// <summary> State when players are in pause menu. </summary>
		Pause, Instruction, End, Lobby, Animation, Shaking,
		/// <summary> Fade out to bright color. </summary>
		/// <remarks> Was 'Lighting' before. </remarks>
		BrightFadeOut,
		/// <summary> Fade in from bright color. </summary>
		/// <remarks> Was 'Unlighting' before. </remarks>
		BrightFadeIn, LobbyInfo, Advice
	};
	/// <summary> Store and control current game state. </summary>
	public static GameState gameState = GameState.StartGame;
	public static int Hint = 0;
	public static int newLevel, currentLevel, lastattack = 0;
	public static int playtimes = 1;
	public static float totalmoney = 0, totalexp = 0, moneycount = 0, expcount = 0, chLevel = 1;
	public static bool battle = false, isMake = false, cleareffect = false, isStory = false, isUser = false, win = false;

#warning I suggest to use enumertaion type for those three fields.
#warning Description of each state is ambigious.
	/// <summary> Unknown. </summary>
	/// <remarks>
	///	<list type="table">
	///	<listheader><term>Value</term><term>Description</term></listheader>
	/// <item><term>0</term><term>unstory</term></item>
	/// <item><term>1</term><term>startstory</term></item>
	/// <item><term>2</term><term>loading</term></item>
	/// <item><term>3</term><term>storydragon</term></item>
	/// <item><term>4</term><term>dragonshow</term></item>
	/// <item><term>5</term><term>house</term></item>
	/// <item><term>6</term><term>light</term></item>
	/// <item><term>7</term><term>&lt;Unknown&gt;</term></item>
	///	</list>
	/// </remarks>
	public static int storystate = 0;
	/// <summary> Unknown. </summary>
	/// <remarks>
	/// <list type="table">
	/// <listheader><term>Value</term><term>Description</term></listheader>
	/// <item><term>0</term><term>null</term></item>
	/// <item><term>1</term><term>big_rain</term></item>
	/// <item><term>2</term><term>light</term></item>
	/// <item><term>3</term><term>&lt;Unknown&gt;</term></item>
	/// <item><term>4</term><term>&lt;Unknown&gt;</term></item>
	/// </list>
	/// </remarks>
	public static int storyeffect = 0;
	/// <summary> Unknown. </summary>
	/// <remarks>
	///	<list type="table">
	///	<listheader><term>Value</term><term>Description</term></listheader>
	/// <item><term>0</term><term>null</term></item>
	/// <item><term>1</term><term>???</term></item>
	/// <item><term>2</term><term>what?</term></item>
	/// <item><term>3</term><term>escape</term></item>
	/// <item><term>4</term><term>rescue</term></item>
	/// <item><term>5</term><term>turn</term></item>
	/// <item><term>6</term><term>slime</term></item>
	///	</list>
	/// </remarks>
	public static int storychat = 0;

	private float delta = 0;

	public GameObject board, wlboard, levelboard, brand, dialogBox, help, pauseButton, potionicon, keyicon, moneyicon, lobbyinfo, turnBack, skip, inputfield, debugcanvas, circleHint, ovalHint;
	public Text moneyicontext;
	public static GameObject keyCountObject;
	public static SpriteRenderer background;

	void Start()
	{
		keyCountObject = GameObject.Find("Key Count");
		background = GetComponent<SpriteRenderer>();
		Instantiate(slimePrefab);
	}

	void Update()
	{
#warning The method to summon and kill the canvases and gameobjects here would cause game to caculate all the expressions in every frame (usually 60 tps), should come up with a better idea.
		/// Show or hide items
		// Canvases
		if(Input.GetMouseButtonDown(0))  circleHint.SetActive(false);
		inputfield.SetActive(storystate == 9 || gameState == GameState.Input);
		skip.SetActive(gameState == GameState.StartStory && storystate >= 3);
		lobbyCanvas.SetActive(isLobby && currentLevel == 0 && !battle);
		wlboard.SetActive(hasEnded);
		levelboard.SetActive(isLobby && currentLevel > 0 || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Lobby);
		passCanvas.SetActive(WLBoardHandler.stmenu && !battle);
		deadCanvas.SetActive(WLBoardHandler.stmenu && battle);
		slimeHealthCanvas.SetActive(isPlaying || isAnimation || gameState == GameState.Shaking || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Playing);
		// GameObjects
		brand.SetActive(gameState == GameState.Lobby && currentLevel > 0 || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Lobby);
		dialogBox.SetActive(isAnimation || gameState == GameState.Advice || (gameState == GameState.StartStory && storychat != 0));
		pauseButton.SetActive(!isPaused && gameState != GameState.StartGame && gameState != GameState.StartStory && gameState != GameState.Loading);
		debugcanvas.SetActive(isPaused && isUser);
		board.SetActive(isPaused);
		help.SetActive(gameState == GameState.Instruction);
		potionicon.SetActive(isPlaying || isAnimation || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Playing);
		keyicon.SetActive(isPlaying || isAnimation || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Playing);
		moneyicon.SetActive(isLobby && currentLevel > 0 || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Lobby);
		lobbyinfo.SetActive(gameState == GameState.LobbyInfo);
		turnBack.SetActive(gameState == GameState.LobbyInfo);

		switch (gameState)
		{
			case GameState.Input:
				DarkAnimatorController.SkipStory();
				break;
			case GameState.StartGame:
				break;
			case GameState.StartStory:
				switch (storyeffect)
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
			case GameState.BrightFadeOut:
				delta += Time.deltaTime;
				if (delta >= 1)
				{
					delta = 0;
					gameState = GameState.BrightFadeIn;
				}
				break;
			case GameState.BrightFadeIn:
				delta += Time.deltaTime;
				if (delta >= 1)
				{
					delta = 0;
					gameState = battle ? GameState.Playing : GameState.Lobby;
				}
				break;
			case GameState.DarkFadeOut:
				cleareffect = false;
				delta += Time.deltaTime;
				if (delta >= 1)
				{
					delta = 0;
					if (!battle && currentLevel > 0)
					{
						if (currentLevel == 1) Instantiate(weather[1]);
						else Instantiate(weather[Random.Range(1, 4)]);
					}
					else if (battle && LevelVarity.LevelWeather[0][currentLevel] != -1) Instantiate(force[LevelVarity.LevelWeather[0][currentLevel]]);
					if (battle && currentLevel == 1) Instantiate(weather[4]).GetComponent<Transform>().position = new Vector3(-30, 56, 0);
					gameState = battle ? GameState.LevelPrepare : GameState.MenuPrepare;
				}
				break;
			case GameState.MenuPrepare:
				guildwoman.startanim = true;
				DarkAnimatorController.start = true;
				background.sprite = menuBackground[currentLevel];
				Hint = 0;
				if(LevelVarity.lobbyHint[currentLevel])  Instantiate(ovalHint).GetComponent<Transform>().position = LevelVarity.lobbyoval[currentLevel-1][0];
				gameState = GameState.Loading;
				break;
			case GameState.LevelPrepare:
				if (LevelVarity.me == null)
					gameState = GameState.Input;
				else
				{
					Slime.keyCount = 0;
					DarkAnimatorController.start = true;
					gameState = GameState.Loading;
				}
				background.sprite = gameBackground[currentLevel];
				Hint = 0;
				if(LevelVarity.playHint[currentLevel] && LevelVarity.me != null)  Instantiate(ovalHint).GetComponent<Transform>().position = LevelVarity.playoval[currentLevel][0];
				break;
			case GameState.DarkFadeIn:
				if (!isMake)
				{
					MakeMap(battle ? 0 : 1);
					isMake = true;
				}
				LifeHandler.start = true;
				delta += Time.deltaTime;
				if (delta >= 1)
				{
					delta = 0;
					isMake = false;
					if (storystate == 0 && !isStory)
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
				moneyicontext.text = " x " + totalmoney;
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

	/// <summary> Called to start a new level. </summary>
	/// <remarks> Could also called to trigger level reset. </remarks>
	public static void StartNewLevel()
	{
		battle = true; // Starts the battle
		gameState = GameState.DarkFadeOut;
	}

	/// <summary> Called to navigate to lobby. </summary>
	public static void GotoLobby()
	{
		battle = false;
		gameState = GameState.DarkFadeOut;
	}

	/// <summary> Called when a level completed. </summary>
	public static void OnLevelComplete()
	{
		newLevel = currentLevel + 1;
		battle = false; // Ends the battle
		keyCountObject.GetComponent<CountLabel>().UpdateCount(0);
		gameState = GameState.End;
	}

	/// <summary> Called when a level failed. </summary>
	public static void OnLevelFail()
	{
		battle = true;
		keyCountObject.GetComponent<CountLabel>().UpdateCount(0);
		gameState = GameState.End;
	}

	void MakeMap(int which)
	{
		if (which == 0)
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
	public static bool isDarking => gameState == GameState.DarkFadeOut;
	public static bool isBrightening => gameState == GameState.DarkFadeIn;
	public static bool hasEnded => gameState == GameState.End;
	public static bool isStart => gameState == GameState.LevelPrepare;

	public static void SetPlaying() { gameState = GameState.Playing; }

	public static void SetAnimation() { gameState = GameState.Animation; }

	/// <summary> Set the current game state. </summary>
	/// <param name="state">The state to set in string.</param>
	public static void SetState(string state)
	{
		gameState = (GameState)System.Enum.Parse(typeof(GameState), state);
	}

	public static void givename()
	{
		for (int i = 0; i < LevelVarity.teller.Count; i++)
		{
			for (int j = 0; j < LevelVarity.teller[i].Count; j++)
			{
				if (LevelVarity.teller[i][j] == LevelVarity.lastname) LevelVarity.teller[i][j] = LevelVarity.me;
			}
		}
		LevelVarity.lastname = LevelVarity.me;
	}
	public void Inputstate()
	{
		gameState = GameState.Input;
	}

}
