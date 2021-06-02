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

    public enum GameState { Start, MenuPrepare, Darking, Brightening, Playing, Pause, Instruction, End, Lobby, Animation, Shaking, Lighting, Unlighting, Interval };
    public static GameState gameState = GameState.MenuPrepare;
    public static int currentLevel = 0;
    public static bool battle = false;
    float delta = 0;

    static GameObject board, brand, dialogBox, help, pauseButton, potionicon, keyicon;
    SpriteRenderer background;

    void Start()
    {
        board = GameObject.Find("Board");
        brand = GameObject.Find("Brand");
        dialogBox = GameObject.Find("Dialog Box");
        help = GameObject.Find("Help");
        pauseButton = GameObject.Find("Pause Button");
        potionicon = GameObject.Find("Potionicon");
        keyicon = GameObject.Find("Keyicon");
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
        slimeHealthCanvas.SetActive(isPlaying || isAnimation);
        // GameObjects
        brand.SetActive(gameState == GameState.Lobby && currentLevel > 0);
        dialogBox.SetActive(isAnimation);
        pauseButton.SetActive(!isPaused);
        board.SetActive(isPaused);
        help.SetActive(gameState == GameState.Instruction);
        potionicon.SetActive(isPlaying || isAnimation);
        keyicon.SetActive(isPlaying || isAnimation);
        
        switch (gameState)
        {
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
                    gameState = battle ? GameState.Start : GameState.MenuPrepare;
                }
                break;
            case GameState.MenuPrepare:
                if (currentLevel != 0)
                {
                    Slime.instance.gameObject.transform.position = new Vector2(1f, 5f);
                    Instantiate(floorPrefab);
                    Instantiate(instructPrefab).transform.position = new Vector2(80f, 8f);
                }
                gameState = GameState.Brightening;
                break;
            case GameState.Start:
                Slime.keyCount = 0;
                SlimeLifeCanvas.life = 6;
                if (currentLevel < LevelVarity.spawnpoint.Count)
                {
                    Slime.transform.position = LevelVarity.spawnpoint[currentLevel];
                    Instantiate(levelPrefab[currentLevel]);
                }
                gameState = GameState.Brightening;
                break;
            case GameState.Brightening:
                background.sprite = battle ? gameBackground[currentLevel] : menuBackground[currentLevel];
                delta += Time.deltaTime;
                if (delta >= 1)
                {
                    delta = 0;
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
        }
    }

    public static void StartNewGame()
    {
        battle = true; // Starts the battle
        Slime.instance.ResetHealth();
        gameState = GameState.Darking;
    }

    public static void GoodEnd()
    {
        battle = false; // Ends the battle
        gameState = GameState.End;
    }
    public static void BadEnd()
    {
        gameState = GameState.End;
    }
    public static bool isPlaying => gameState == GameState.Playing;
    public static bool isPaused => gameState == GameState.Pause;
    public static bool isLobby => gameState == GameState.Lobby;
    public static bool isAnimation => gameState == GameState.Animation;
    public static bool isMenuPrepare => gameState == GameState.MenuPrepare;
    public static bool isDarking => gameState == GameState.Darking;
    public static bool hasEnded => gameState == GameState.End;
}
