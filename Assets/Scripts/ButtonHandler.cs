using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject slimePrefab;
    public void GameInit()
    {
        MainCameraHandler.allSound = 3;
        LifeHandler.targetlife = LifeHandler.entitylife;
        LifeHandler.lastlife = LifeHandler.entitylife;
        LifeHandler.tghealamount = 0;
        LifeHandler.tgsufferamount = 0;
        GameGlobalController.StartNewGame();
    }
    /*public void NextLevel()
    {
        GameGlobalController.currentLevel++;
        GameGlobalController.gameState = GameGlobalController.GameState.Darking;
    }*/
    public void ShowLobby()
    {
        if (!GameGlobalController.battle) GameGlobalController.currentLevel++;
        GameGlobalController.battle = false;
        MainCameraHandler.allSound = 3;
        GameGlobalController.gameState = GameGlobalController.GameState.Darking;
    }
}
