using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameGlobalController.gameState == GameGlobalController.GameState.MenuPrepare || GameGlobalController.gameState == GameGlobalController.GameState.Darking){
            DialogBoxHandler.cbnum=0;
            Animation.handler.handle();
            Destroy(gameObject);
        }
    }
}
