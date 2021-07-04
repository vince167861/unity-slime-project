using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainLightingHandler : MonoBehaviour
{
    private int num = 0;
    public GameObject Lightning;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch(GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Lobby:
            case GameGlobalController.GameState.Playing:
            case GameGlobalController.GameState.StartStory:
                num = Random.Range(1,101);
                if(num == 50)
                {
                    num = 0;
                    Vector3 positionA = new Vector3(Random.Range(10,111),35,0);
                    Quaternion rotationA = Quaternion.Euler(0, 0, Random.Range(-20, 21));
                    Instantiate(Lightning, positionA, rotationA);
                }
            break;
        }
    }
}
