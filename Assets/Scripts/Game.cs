using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	/// Imports
	// Canvases
	public GameObject passCanvas, deadCanvas, slimeHealthCanvas, mapCanvas, bottonCanvas, endCanvas;
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
		StartGame, Story, Input,
		/// <summary> State before every level starts. </summary>
		/// <remarks> Was 'Start' before. </remarks>
		LevelPrepare,
		/// <summary> State before enter menu. </summary>
		MenuPrepare,
		/// <summary> Fade out to dark color. </summary>
		/// <remarks> Was 'Darking' before.</remarks>
		DarkFadeOut,
		/// <summary> Loading is between DarkFadeIn and DarkFadeOut. </summary>
		Loading,
		/// <summary> Fade in from dark color. </summary>
		/// <remarks> Was 'Brightening' before. </remarks>
		DarkFadeIn,
		/// <summary> State when players are playing in every level. </summary>
		Playing,
		/// <summary> State when players are in pause menu. </summary>
		Pause, Instruction, End, Lobby, Dialog, Shaking,
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
	public static int newLevel, currentLevel, lastattack = 0;  //{0:bird;1:pipi;2:trap;3:sk}
	public static int playtimes = 1;
	public static float totalmoney = 0, totalexp = 0, moneycount = 0, expcount = 0, chLevel = 1;
	public static bool battle = false, debugMode = true, win = false, startmenu = false;
	static bool hasEffectInstantiated = false;
	public enum StoryState { NoStory, StartStory, Loading, StoryDragon, DragonShow, House, Light, State7, State8, State9 };
	public static StoryState storyState = StoryState.NoStory;
	public enum StoryEffect { Clear, ThunderStorm, Light, GroundOfFire, Effect4, None};
	public static StoryEffect storyEffect = StoryEffect.Clear;
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

	float delta = 0;

	public GameObject board, wlboard, levelboard, brand, dialogBox, help, pauseButton, potionicon, keyicon, moneyicon, lobbyinfo, turnBack, skip, inputfield, debugcanvas, circleHint, ovalHint;
	public Text moneyicontext;
	public static SpriteRenderer background;
	public static GameObject keyCountObject, staticFloorPrefab;
	public static GameObject[] staticEffect, staticLevelPrefab;

	void Start()
	{
		keyCountObject = GameObject.Find("Key Count");
		background = GetComponent<SpriteRenderer>();
		Instantiate(slimePrefab);
		staticEffect = weather;
		staticLevelPrefab = levelPrefab;
		staticFloorPrefab = floorPrefab;
	}

	void Update()
	{
		// TODO: The method to summon and kill the canvases and gameobjects here would cause game to caculate all the expressions in every frame (usually 60 tps), should come up with a better idea.
		/// Show or hide items
		// Canvases
		endCanvas.SetActive(gameState == GameState.Pause || startmenu);
		bottonCanvas.SetActive(gameState == GameState.Playing || gameState == GameState.Lobby);
		if (Input.GetMouseButtonDown(0)) circleHint.SetActive(false);
		inputfield.SetActive(storyState == StoryState.State9 || gameState == GameState.Input);
		skip.SetActive(gameState == GameState.Story && (int)storyState >= 3);
		wlboard.SetActive(hasEnded);
		levelboard.SetActive(isLobby && currentLevel > 0 || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Lobby);
		passCanvas.SetActive(WLBoardHandler.stmenu && !battle);
		deadCanvas.SetActive(WLBoardHandler.stmenu && battle);
		slimeHealthCanvas.SetActive(isPlaying || IsDialog || gameState == GameState.Shaking || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Playing);
		// GameObjects
		brand.SetActive(gameState == GameState.Lobby && currentLevel > 0 || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Lobby);
		dialogBox.SetActive(IsDialog || gameState == GameState.Advice || (gameState == GameState.Story && storychat != 0));
		pauseButton.SetActive(!isPaused && gameState != GameState.StartGame && gameState != GameState.Story && gameState != GameState.Loading);
		debugcanvas.SetActive(isPaused && debugMode);
		board.SetActive(isPaused);
		help.SetActive(gameState == GameState.Instruction);
		potionicon.SetActive(isPlaying || IsDialog || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Playing);
		keyicon.SetActive(isPlaying || IsDialog || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Playing);
		moneyicon.SetActive(isLobby && currentLevel > 0 || gameState == GameState.Advice && DialogBoxHandler.lastgameState == GameState.Lobby);
		lobbyinfo.SetActive(gameState == GameState.LobbyInfo);
		turnBack.SetActive(gameState == GameState.LobbyInfo);

		switch (gameState)
		{
			case GameState.Input:
				ScreenCover.SkipStory();
				break;
			case GameState.Story:
				if (storyEffect != StoryEffect.Clear && !hasEffectInstantiated)
				{
					hasEffectInstantiated = true;
					switch (storyEffect)
					{
						case StoryEffect.ThunderStorm:
							Instantiate(weather[0]);
							PreLoading();
							break;
						case StoryEffect.Light:
							Instantiate(weather[6]);
							break;
						case StoryEffect.GroundOfFire:
							Instantiate(weather[5]);
							break;
						case StoryEffect.Effect4:
							Instantiate(force[1]);
							break;
					}
				}
				if (storyEffect == StoryEffect.Clear && hasEffectInstantiated)
					hasEffectInstantiated = false;
				break;
			case GameState.MenuPrepare:
				GuildWoman.otheradvice = true;
				GuildWoman.startanim = true;
				background.sprite = menuBackground[currentLevel];
				Hint = 0;
				if (DataStorage.lobbyHint[currentLevel])
					Instantiate(ovalHint, DataStorage.lobbyoval[currentLevel - 1][0], Quaternion.identity);
				PreLoading();
				break;
			case GameState.LevelPrepare:
				if (DataStorage.me == null)
					gameState = GameState.Input;
				else
				{
					Slime.keyCount = 0;
          background.sprite = gameBackground[currentLevel];
          Hint = 0;
					PreLoading();
				}
				break;
      case GameState.BrightFadeOut:
        delta += Time.deltaTime; if (delta >= 1) { delta = 0; PostBrightFadeOut(); } break;
      case GameState.BrightFadeIn:
        delta += Time.deltaTime; if (delta >= 1) { delta = 0; PostBrightFadeIn(); } break;
      case GameState.DarkFadeOut:
	  	storyEffect = StoryEffect.Effect4;
        delta += Time.deltaTime; if (delta >= 1) { delta = 0; PostDarkFadeOut(); } break;
			case GameState.DarkFadeIn:
				delta += Time.deltaTime; if (delta >= 1) { delta = 0; PostDarkFadeIn(); } break;
		}
	}

	// DarkFadeOut
	public static void PreDarkFadeOut()
	{
		gameState = GameState.DarkFadeOut;
		ScreenCover.PreDarkFadeOut();
		//storyEffect = StoryEffect.Clear;
	}
	void PostDarkFadeOut()
	{
		if (!battle && currentLevel > 0)
		{
			if (currentLevel == 1) Instantiate(weather[1]);
			else Instantiate(weather[Random.Range(1, 4)]);
		}
		else if (battle && DataStorage.LevelWeather[0][currentLevel] != -1)
			Instantiate(force[DataStorage.LevelWeather[0][currentLevel]]);
		if (battle && currentLevel == 1)
			Instantiate(weather[4], new Vector3(-30, 56, 0), Quaternion.identity);
		gameState = battle ? GameState.LevelPrepare : GameState.MenuPrepare;
	}

	// DarkFadeIn
	public static void PreDarkFadeIn()
	{
		gameState = GameState.DarkFadeIn;
		ScreenCover.PreDarkFadeIn();
		MakeMap(battle);
		Map.UpdateMap();
		MainCharacterHealth.start = true;
	}
	void PostDarkFadeIn()
	{
		gameState = battle ? GameState.Playing : GameState.Lobby;
	}

	// Loading
	public static void PreLoading()
	{
		gameState = GameState.Loading;
		Slime.PreLoading();
		ScreenCover.PreLoading();
	}

	// BrightFadeOut
	public static void PreBrightFadeOut()
	{
    gameState = GameState.BrightFadeOut;
    ScreenCover.PreBrightFadeOut();
  }
	void PostBrightFadeOut()
	{
    PreBrightFadeIn();
  }

	// BrightFadeIn
	public static void PreBrightFadeIn()
	{
    gameState = GameState.BrightFadeIn;
    ScreenCover.PreBrightFadeIn();
  }
	void PostBrightFadeIn()
  {
    gameState = battle ? GameState.Playing : GameState.Lobby;
	}

	/// <summary> Called to start a new level. </summary>
	/// <remarks> Could also called to trigger level reset. </remarks>
	public static void StartNewLevel()
	{
		battle = true; // Starts the battle
		PreDarkFadeOut();
	}

	/// <summary> Called to navigate to lobby. </summary>
	public static void GotoLobby()
	{
		battle = false;
		PreDarkFadeOut();
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

	static void MakeMap(bool isInBattle)
	{
		if (isInBattle)
		{
			if (currentLevel < DataStorage.spawnpoint.Count)
			{
				Slime.instance.transform.position = DataStorage.spawnpoint[currentLevel];
				Instantiate(staticLevelPrefab[currentLevel]);
			}
		}
		else
		{
			if (currentLevel != 0)
			{
				Slime.instance.transform.position = new Vector2(1f, 5f);
				Instantiate(staticFloorPrefab);
			}
		}
	}

	public static bool isPlaying => gameState == GameState.Playing;
	public static bool isPaused => gameState == GameState.Pause;
	public static bool isLobby => gameState == GameState.Lobby;
	public static bool IsDialog => gameState == GameState.Dialog;
	public static bool isMenuPrepare => gameState == GameState.MenuPrepare;
	public static bool isDarking => gameState == GameState.DarkFadeOut;
	public static bool isBrightening => gameState == GameState.DarkFadeIn;
	public static bool hasEnded => gameState == GameState.End;
	public static bool isStart => gameState == GameState.LevelPrepare;
	public static bool isLoading => gameState == GameState.Loading;
	public static bool isStory => gameState == GameState.Story;

	public static void SetPlaying() { gameState = GameState.Playing; }
	public static void SetDialog() { gameState = GameState.Dialog; }

	public static void Rename()
	{
		DataStorage.speakerName = DataStorage.speakerName.ConvertAll(name => name == DataStorage.lastname ? DataStorage.me : name);
		DataStorage.lastname = DataStorage.me;
	}
	public void Inputstate()
	{
		gameState = GameState.Input;
	}

}
