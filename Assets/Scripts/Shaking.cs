using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    Transform camera_T;
    float delta = 0;
    bool dragon=false;
    public GameObject dragonPrefab;
    public static float[] timer = { 0, 0.2f, 0.4f, 0.6f, 0.8f, 0.9f, 1.0f, 1.1f, 1.2f, 1.3f, 1.4f, 1.8f, 2.2f};
    // Start is called before the first frame update
    void Start()
    {
        camera_T = Camera.main.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Shaking:
                this.delta += Time.deltaTime;
                if (this.delta >= timer[12])
                    {
                        GameGlobalController.gameState = GameGlobalController.GameState.Animation;
                        this.delta = 0;
                    }
                if (this.delta >= timer[10] && this.delta <= timer[11])
                    {
                        if(!this.dragon){
                            Instantiate(dragonPrefab).GetComponent<Transform>().position = new Vector2(16, 69);
                            this.dragon=true;
                        }
                        MainCameraHandler.targetPosition = new Vector3(16, 69, -10);
                    }
                for (int i = 0; i <= 11; i++)
                {
                    if (this.delta >= timer[i] && this.delta <= timer[i + 1]&&i!=10)
                    {
                        MainCameraHandler.targetPosition = camera_T.position + new Vector3(0, ((i % 2) * -2 + 1), 0);
                    }
                }
                break;
        }

    }
}
