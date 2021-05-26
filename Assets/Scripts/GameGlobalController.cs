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

    static GameObject board, brand, dialogBox, help, pauseButton;
    // Global should exists only a slime instance.
    public static GameObject slimeInstance = null;
    SpriteRenderer background;

    void Start()
    {
        board = GameObject.Find("Board");
        brand = GameObject.Find("Brand");
        dialogBox = GameObject.Find("Dialog Box");
        help = GameObject.Find("Help");
        pauseButton = GameObject.Find("Pause Button");
        background = GetComponent<SpriteRenderer>();
        slimeInstance = Instantiate(slimePrefab);
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
                    slimeInstance.transform.position = new Vector2(1f, 5f);
                    Instantiate(floorPrefab);
                    Instantiate(instructPrefab).transform.position = new Vector2(80f, 8f);
                }
                gameState = GameState.Brightening;
                break;
            case GameState.Start:
                SlimeLifeCanvas.life = 6;
                if (currentLevel < LevelVarity.spawnpoint.Count)
                {
                    slimeInstance.transform.position = LevelVarity.spawnpoint[currentLevel];
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
                if (slimeInstance.GetComponent<Slime>().health == 0) gameState = GameState.End;
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
        slimeInstance.GetComponent<Slime>().Reset();
        gameState = GameState.Darking;
    }

    public static void GoodEnd()
    {
        battle = false; // Ends the battle
        gameState = GameState.End;
    }
    public static bool isPlaying { get => gameState == GameState.Playing; }
    public static bool isPaused { get => gameState == GameState.Pause; }
    public static bool isLobby { get => gameState == GameState.Lobby; }
    public static bool isAnimation { get => gameState == GameState.Animation; }
    public static bool isMenuPrepare { get => gameState == GameState.MenuPrepare; }
    public static bool isDarking { get => gameState == GameState.Darking; }
    public static bool hasEnded { get => gameState == GameState.End; }
}
