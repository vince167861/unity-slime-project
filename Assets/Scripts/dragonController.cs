using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch(GameGlobalController.gameState){
            case GameGlobalController.GameState.BrightFadeIn:
                Destroy(gameObject);
                break;
        }
    }
}
