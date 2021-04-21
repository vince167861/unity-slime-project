using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopbottomh : MonoBehaviour
{
    GameGlobalController.GameState nowState;
    // Start is called before the first frame update
    public void GameStop()
    {
        nowState = GameGlobalController.gameState;
        GameGlobalController.gameState = GameGlobalController.GameState.Pause;
    }
    public void GameStart()
    {
        GameGlobalController.gameState = nowState;
        //test
    }
}
