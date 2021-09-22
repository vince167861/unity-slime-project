using UnityEngine;

public class DebugMenu : MonoBehaviour
{
	public void GotoL(int lvl)
	{
		Game.currentLevel = lvl;
		DialogBoxHandler.dialogID = 0;
		Game.StartNewLevel();
	}

	public void GotoLobby()
	{
		Game.GotoLobby();
	}

	public void GotoLobby_add()
	{
		Game.currentLevel++;
		Game.GotoLobby();
	}
}
