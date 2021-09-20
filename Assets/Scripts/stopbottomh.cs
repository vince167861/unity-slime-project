using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stopbottomh : MonoBehaviour
{
    public Sprite[] stop = new Sprite[2];
    Game.GameState nowState,prenowState;
    // Start is called before the first frame update
    public void GameStop()
    {
        MainCameraHandler.PlayEntityClip(3);
        if(Game.gameState == Game.GameState.Instruction)
        {
            Game.gameState = Game.GameState.Pause;
        }
        else
        {
            nowState = Game.gameState;
            Game.gameState = Game.GameState.Pause;
        }
    }
    public void GameStart()
    {
        MainCameraHandler.PlayEntityClip(3);
        Game.gameState = nowState;
    }
    void Start()
    {
    }
    void Update(){
        if(Game.gameState == Game.GameState.Instruction){
            if(Game.currentLevel == 0 && DialogBoxHandler.dialogID == 5)    nowState = Game.GameState.Playing;
            gameObject.GetComponent<Image>().sprite = stop[1];
        }
        else gameObject.GetComponent<Image>().sprite = stop[0];
    }
}
