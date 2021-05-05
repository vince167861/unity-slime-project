using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    public void GotoL(int lvl)
    {
        GameGlobalController.currentLevel = lvl;
        GameGlobalController.StartNewGame();
    }
}
