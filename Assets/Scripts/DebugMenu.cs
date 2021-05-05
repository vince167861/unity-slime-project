using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    public void GotoL2()
    {
        GameGlobalController.currentLevel = 1;
        GameGlobalController.StartNewGame();
    }
}
