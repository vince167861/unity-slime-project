using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopbottomh : MonoBehaviour
{
    public void GameStop()
    {
      GameGlobalController.gameState = GameGlobalController.GameState.Pause;
    }
    public void GameStart()
    {
      GameGlobalController.gameState = GameGlobalController.GameState.Playing;
    }
    public void MusicTo0()
    {
      
    }
}
