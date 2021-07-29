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
		Game.chLevel = (float)System.Math.Round(Game.totalexp + WLBoardHandler.expamount / WLBoardHandler.needexp);
		Game.totalmoney += Game.moneycount;
		Game.totalexp += Game.expcount;
		WLBoardHandler.expamount = 0;
		WLBoardHandler.moneyamount = 0;
		WLBoardHandler.stmenu = false;
		MainCameraHandler.allSound = 3;
		if (Game.storystate == 1)
		{
			DarkAnimatorController.animator.Play("Start Story");
			Game.cleareffect = true;
			Game.gameState = Game.GameState.StartStory;
		}
		else
			Game.StartNewLevel();
	}
	/*public void NextLevel()
	{
			GameGlobalController.currentLevel++;
			GameGlobalController.gameState = GameGlobalController.GameState.Darking;
	}*/
	public void ShowLobby()
	{
		if (!Game.battle) Game.currentLevel++;
		Game.battle = false;
		MainCameraHandler.allSound = 3;
		Game.gameState = Game.GameState.DarkFadeOut;
	}
}
