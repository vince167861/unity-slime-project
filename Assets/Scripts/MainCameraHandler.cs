using UnityEngine;

public class MainCameraHandler : MonoBehaviour
{
	public AudioClip[] entityclip, backgroundclip, lobbyclip, storyclip;
	public AudioSource audiosource;
	public static float prevolume = 0.5f;
	public static AudioClip[] staticEntityClip, staticBackgroundClip, staticLobbyClip, staticStoryClip;
	public static AudioSource staticAudioSource;
	public static Vector3 targetPosition;
	public float cameraSpeedFactor = 10;
#warning Please add xml comment for those three fields.
	public static float soundLoud = 0;
	public static int storymusic = 0;
	public static bool hasMusicPlaying = false;
	public GameObject clickPrefab;

	Vector2 scrCtrPos;
	Vector3 mousePosition;
	SpriteRenderer backgroundSpriteRenderer;

	// Start is called before the first frame update
	void Start()
	{
		staticAudioSource = audiosource = GetComponent<AudioSource>();
		scrCtrPos = new Vector2(Screen.width / 2, Screen.height / 2);
		backgroundSpriteRenderer = GameObject.Find("Background").GetComponent<SpriteRenderer>();
		staticEntityClip = entityclip;
		staticBackgroundClip = backgroundclip;
		staticLobbyClip = lobbyclip;
		staticStoryClip = storyclip;
	}

	void Update()
	{
		// Update mouse position
		mousePosition = Input.mousePosition;
		audiosource.volume = prevolume;
		// Place camera in right position
		switch (Game.gameState)
		{
			case Game.GameState.Story:
			case Game.GameState.Loading:
				PlayLoopClip();
				transform.position = new Vector3(0, 0, -10);
				break;
			case Game.GameState.LevelPrepare:
			case Game.GameState.MenuPrepare:
				targetPosition = new Vector3(33, 24, -10);
				hasMusicPlaying = false;
				audiosource.Stop();
				break;
			case Game.GameState.Playing:
			case Game.GameState.Lobby:
				PlayLoopClip();
				break;
		}

		if (Game.gameState != Game.GameState.StartGame && Game.gameState != Game.GameState.Story)
		{
			// Update camera view position
			// float camera_x = targetPosition.x + mouseDeltaX, camera_y = targetPosition.y + mouseDeltaY;
			float camera_x = targetPosition.x, camera_y = targetPosition.y;
			// Fix the position to prevent rendering void
			if (camera_x < CameraCtrX) camera_x = CameraCtrX;
			if (camera_x > BackgroundSizeX - CameraCtrX) camera_x = BackgroundSizeX - CameraCtrX;
			if (camera_y < CameraCtrY) camera_y = CameraCtrY;
			if (camera_y > BackgroundSizeY - CameraCtrY) camera_y = BackgroundSizeY - CameraCtrY;
			// Move towards the target point
			Vector3 newTarget = new Vector3(camera_x, camera_y, targetPosition.z);
			transform.position = Vector3.MoveTowards(transform.position, newTarget, Vector3.Distance(transform.position, newTarget) * cameraSpeedFactor / 100);

			// Show ripple
			if (Input.GetMouseButtonDown(0))
				Instantiate(clickPrefab).GetComponent<Transform>().position =
						new Vector2(MouseDeltaX * CameraCtrX * 2 + transform.position.x, MouseDeltaY * CameraCtrY * 2 + transform.position.y);
		}
	}

	// Utilities
	float MouseDeltaX => (mousePosition.x - scrCtrPos.x) / Screen.width;
	float MouseDeltaY => (mousePosition.y - scrCtrPos.y) / Screen.height;
	float CameraCtrX => Camera.main.orthographicSize * Camera.main.aspect;
	float CameraCtrY => Camera.main.orthographicSize;
	float BackgroundSizeX => backgroundSpriteRenderer.bounds.size.x;
	float BackgroundSizeY => backgroundSpriteRenderer.bounds.size.y;

	public static void PlayEntityClip(int val)
	{
		staticAudioSource.PlayOneShot(staticEntityClip[val]);
	}

	public static void PlayLoopClip(bool overwrite = false)
	{
		if (!(hasMusicPlaying || overwrite))
		{
			staticAudioSource.clip = (Game.isLoading || Game.isStory) && (storymusic != 0) ? staticStoryClip[storymusic] : Game.isPlaying ? staticBackgroundClip[Game.currentLevel] : staticLobbyClip[Game.currentLevel];
			staticAudioSource.loop = true;
			staticAudioSource.Play();
			hasMusicPlaying = true;
		}
	}
}
