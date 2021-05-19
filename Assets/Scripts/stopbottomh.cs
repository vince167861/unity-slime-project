using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopbottomh : MonoBehaviour
{
    public Sprite[] stop = new Sprite[2];
    GameGlobalController.GameState nowState,prenowState;
    // Start is called before the first frame update
    public void GameStop()
    {
        nowState = GameGlobalController.gameState;
        prenowState = nowState;
        GameGlobalController.gameState = GameGlobalController.GameState.Pause;
    }
    public void GameStart()
    {
        nowState = prenowState;
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
