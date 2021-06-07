using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stopbottomh : MonoBehaviour
{
    public Sprite[] stop = new Sprite[2];
    GameGlobalController.GameState nowState,prenowState;
    // Start is called before the first frame update
    public void GameStop()
    {
        MainCameraHandler.allSound = 3;
        if(GameGlobalController.gameState == GameGlobalController.GameState.Instruction)
        {
            GameGlobalController.gameState = GameGlobalController.GameState.Pause;
        }
        else
        {
            nowState = GameGlobalController.gameState;
            GameGlobalController.gameState = GameGlobalController.GameState.Pause;
        }
    }
    public void GameStart()
    {
        MainCameraHandler.allSound = 3;
        GameGlobalController.gameState = nowState;
    }
    void Start()
    {
    }
    void Update(){
        if(GameGlobalController.gameState == GameGlobalController.GameState.Instruction){
            gameObject.GetComponent<Image>().sprite = stop[1];
        }
        else gameObject.GetComponent<Image>().sprite = stop[0];
    }
}
