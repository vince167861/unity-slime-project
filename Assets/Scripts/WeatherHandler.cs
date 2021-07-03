using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameGlobalController.cleareffect)
        {
            Destroy(gameObject);
        }
        switch(GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Darking:
                Destroy(gameObject);
                break;
        }
    }
}
