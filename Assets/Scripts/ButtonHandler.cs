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
		GameGlobalController.cleareffect = true;
		DarkAnimatorController.animator.SetBool("skip", true);
		GameGlobalController.storystate = 0;
		GameGlobalController.battle = true;
		GameGlobalController.gameState = GameGlobalController.GameState.LevelPrepare;
	}

	public void GameInit()
	{
		MainCameraHandler.allSound = 3;
		if (GameGlobalController.storystate == 1)
		{
			DarkAnimatorController.animator.Play("Start Story");
			GameGlobalController.cleareffect = true;
			GameGlobalController.gameState = GameGlobalController.GameState.StartStory;
		}
		else
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
		GameGlobalController.gameState = GameGlobalController.GameState.DarkFadeOut;
	}
}
