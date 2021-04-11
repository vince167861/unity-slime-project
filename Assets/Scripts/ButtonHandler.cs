using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject slimePrefab;
    public void GameInit()
    {
        MainCameraHandler.allSound=3;
        GameGlobalController.GameReset();
    }
    public void GameLossInit()
    {
        Instantiate(slimePrefab);
        MainCameraHandler.allSound=3;
        GameGlobalController.GameReset();
    }
    /*public void NextLevel()
    {
        GameGlobalController.currentLevel++;
        GameGlobalController.gameState = GameGlobalController.GameState.Darking;
    }*/
    public void ShowLobby()
    {
        MainCameraHandler.allSound=3;
        if(!GameGlobalController.battle) GameGlobalController.currentLevel++;
        GameGlobalController.gameState = GameGlobalController.GameState.Darking;
    }
}
