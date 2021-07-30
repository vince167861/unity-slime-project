using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
	public void SkipStory()
	{
		DarkAnimatorController.SkipStory();
		Destroy(GameObject.Find("DragonPrefab(Clone)"));
		Destroy(GameObject.Find("房子內部(Clone)"));
		Game.cleareffect = true;
		DarkAnimatorController.animator.SetBool("skip", true);
		Game.storystate = 0;
		Game.battle = true;
		Game.gameState = Game.GameState.LevelPrepare;
	}

	public void GameInit()
	{
		MainCameraHandler.allSound = 3;
		if (Game.storystate == 1)
		{
			DarkAnimatorController.animator.Play("Start Story");
			Game.cleareffect = true;
			Game.gameState = Game.GameState.StartStory;
		}
		else
		{
			Game.StartNewLevel();
			Game.playtimes++;
		}
	}
	/*public void NextLevel()
	{
			Game.currentLevel++;
			Game.gameState = Game.GameState.Darking;
	}*/
	public void ShowLobby()
	{
		Game.playtimes = 1;
		if (!Game.battle || Game.newLevel > Game.currentLevel) Game.currentLevel++;
		Game.battle = false;
		MainCameraHandler.allSound = 3;
		Game.gameState = Game.GameState.DarkFadeOut;
	}
	public void ChangeValue()
	{
		/*Game.chLevel = (int)Mathf.Floor(Game.chLevel + (Game.totalexp + WLBoardHandler.expamount)/WLBoardHandler.needexp);
		if((Game.totalexp + WLBoardHandler.expamount)/WLBoardHandler.needexp >= 1)
		{
			WLBoardHandler.needexp += 10*(float)(Mathf.Pow(Game.chLevel,1.5f));
			WLBoardHandler.lastnexp += 10*(float)(Mathf.Pow(Game.chLevel - 1, 1.5f));
		}*/
		Game.totalmoney += WLBoardHandler.moneyamount;
		Game.totalexp += WLBoardHandler.expamount;
		WLBoardHandler.expamount = 0;
		WLBoardHandler.moneyamount = 0;
		WLBoardHandler.stmenu = false;
	}
}
