using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraHandler : MonoBehaviour
{
	public AudioClip[] entityclip, backgroundclip, lobbyclip, storyclip;
	public AudioSource audiosource;
	public static float prevolume = 0.5f;
	public static int allSound = 0;
	public static Vector3 targetPosition;
	public float cameraSpeedFactor = 10;
#warning Please add xml comment for those three fields.
	bool music, musicstory = false;
	public static float soundLoud = 0;
	public static int storymusic = 0;
	public GameObject clickPrefab;

	Vector2 scrCtrPos;
	Vector3 mousePosition;
	SpriteRenderer backgroundSpriteRenderer;

	// Start is called before the first frame update
	void Start()
	{
		audiosource = GetComponent<AudioSource>();
		scrCtrPos = new Vector2(Screen.width / 2, Screen.height / 2);
		backgroundSpriteRenderer = GameObject.Find("Background").GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		// Update mouse position
		mousePosition = Input.mousePosition;
		audiosource.volume = prevolume;
		// Place camera in right position
		switch (Game.gameState)
		{
			case Game.GameState.StartStory:
			case Game.GameState.Loading:
				if (storymusic != 0) audiosource.clip = storyclip[storymusic];
				if (!musicstory)
				{
					audiosource.loop = true;
					audiosource.Play();
					musicstory = true;
				}
				transform.position = new Vector3(29f, 21f, -10f);
				break;
			case Game.GameState.LevelPrepare:
			case Game.GameState.MenuPrepare:
				targetPosition = new Vector3(33f, 24f, -10f);
				music = false;
				audiosource.Stop();
				break;
			case Game.GameState.Playing:
			case Game.GameState.Lobby:
				audiosource.clip = Game.isPlaying ? backgroundclip[Game.currentLevel] : lobbyclip[Game.currentLevel];
				if (!music)
				{
					audiosource.loop = true;
					audiosource.Play();
					music = true;
				}
				break;
		}
		if (allSound > 0)
		{
			audiosource.PlayOneShot(entityclip[allSound]);
			allSound = 0;
		}

		if (Game.gameState != Game.GameState.StartGame && Game.gameState != Game.GameState.StartStory)
		{
			// Update camera view position
			// float camera_x = targetPosition.x + mouseDeltaX, camera_y = targetPosition.y + mouseDeltaY;
			float camera_x = targetPosition.x, camera_y = targetPosition.y;
			// Fix the position to prevent rendering void
			if (camera_x < cameraCtrX) camera_x = cameraCtrX;
			if (camera_x > backgroundSizeX - cameraCtrX) camera_x = backgroundSizeX - cameraCtrX;
			if (camera_y < cameraCtrY) camera_y = cameraCtrY;
			if (camera_y > backgroundSizeY - cameraCtrY) camera_y = backgroundSizeY - cameraCtrY;
			// Move towards the target point
			Vector3 newTarget = new Vector3(camera_x, camera_y, targetPosition.z);
			transform.position = Vector3.MoveTowards(transform.position, newTarget, Vector3.Distance(transform.position, newTarget) * cameraSpeedFactor / 100);

			// Show ripple
			if (Input.GetMouseButtonDown(0))
				Instantiate(clickPrefab).GetComponent<Transform>().position =
						new Vector2(mouseDeltaX * cameraCtrX * 2 + transform.position.x, mouseDeltaY * cameraCtrY * 2 + transform.position.y);
		}
	}

	// Utilities
	float mouseDeltaX => (mousePosition.x - scrCtrPos.x) / Screen.width;
	float mouseDeltaY => (mousePosition.y - scrCtrPos.y) / Screen.height;
	float cameraCtrX => Camera.main.orthographicSize * Camera.main.aspect;
	float cameraCtrY => Camera.main.orthographicSize;
	float backgroundSizeX => backgroundSpriteRenderer.bounds.size.x;
	float backgroundSizeY => backgroundSpriteRenderer.bounds.size.y;
}
