using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraHandler : MonoBehaviour
{
    public AudioClip[] entityclip= new AudioClip[5];
    public AudioClip[] backgroundclip= new AudioClip[5];
    public AudioClip[] lobbyclip= new AudioClip[2];
    public AudioSource audiosource;
    public static int allSound = 0;
    public static Vector3 targetPosition;
    public float cameraSpeedFactor = 10;
    bool music=false;
    public static float soundLoud = 0;
    public GameObject clickPrefab;

    Vector2 scrCtrPos;
    SpriteRenderer backgroundSpriteRenderer;
    Vector3 mousePosition;

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
        // Place camera in right position
        switch (GameGlobalController.gameState){
            case GameGlobalController.GameState.Start:
            case GameGlobalController.GameState.MenuPrepare:
                targetPosition = new Vector3(48.0f, 32.0f, -10f);
                this.music = false;
                audiosource.Stop();
                break;
            case GameGlobalController.GameState.Playing:
            case GameGlobalController.GameState.Lobby:
                if (GameGlobalController.isPlaying)
                    audiosource.clip = backgroundclip[GameGlobalController.currentLevel];
                else
                    audiosource.clip = lobbyclip[GameGlobalController.currentLevel];
                if (!this.music)
                {
                    audiosource.loop = true;
                    audiosource.Play();
                    this.music = true;
                }
                break;
        }
        if(allSound>0)
        {
            audiosource.PlayOneShot(entityclip[allSound]);
            allSound=0;
        }

        // Update camera view position
        Vector3 mPos = Input.mousePosition;
        float camera_x = targetPosition.x + mouseDeltaX;
        float camera_y = targetPosition.y + mouseDeltaY;
        // Fix the position to prevent rendering void
        if (camera_x < cameraCtrX) camera_x = cameraCtrX;
        if (camera_x > backgroundSizeX - cameraCtrX) camera_x = backgroundSizeX - cameraCtrX;
        if (camera_y < cameraCtrY) camera_y = cameraCtrY;
        if (camera_y > backgroundSizeY - cameraCtrY) camera_y = backgroundSizeY - cameraCtrY;
        // Move towards the target point
        Vector3 newTarget = new Vector3(camera_x, camera_y, targetPosition.z);
        transform.position = Vector3.MoveTowards(transform.position, newTarget, Vector3.Distance(transform.position, newTarget) * cameraSpeedFactor / 100);
        
        // Show ripple
        if(Input.GetMouseButtonDown(0))
            Instantiate(clickPrefab).GetComponent<Transform>().position =
                new Vector2(mouseDeltaX * cameraCtrX * 2 + transform.position.x, mouseDeltaY * cameraCtrY * 2 + transform.position.y);
    }

    // Utilities
    float mouseDeltaX { get => (mousePosition.x - scrCtrPos.x) / Screen.width; }
    float mouseDeltaY { get => (mousePosition.y - scrCtrPos.y) / Screen.height; }
    float cameraCtrX { get => Camera.main.orthographicSize * Camera.main.aspect; }
    float cameraCtrY { get => Camera.main.orthographicSize; }
    float backgroundSizeX { get => backgroundSpriteRenderer.bounds.size.x; }
    float backgroundSizeY { get => backgroundSpriteRenderer.bounds.size.y; }
}
