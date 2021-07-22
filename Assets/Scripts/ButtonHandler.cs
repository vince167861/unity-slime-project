using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject slimePrefab;

    public void StorySkip()
    {
        DarkAnimatorController.skip();
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
        if(GameGlobalController.storystate == 1)
        {
            MainCameraHandler.allSound = 3;
            GameGlobalController.gameState = GameGlobalController.GameState.StartStory;
            DarkAnimatorController.animator.Play("startstory");
		    GameGlobalController.cleareffect = true;
        }
        else
        {
            MainCameraHandler.allSound = 3;
            GameGlobalController.StartNewGame();
        }
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
