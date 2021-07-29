using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    public void GotoL(int lvl)
    {
        Game.currentLevel = lvl;
        Game.StartNewLevel();
    }

    public void GotoLobby()
    {
        Game.GotoLobby();
    }
}
