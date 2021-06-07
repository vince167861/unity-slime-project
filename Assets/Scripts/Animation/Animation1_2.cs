using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation1_2 : Animation
{
    float delta = 0;
    public static bool[] initialPoint = { false, false, false, false, false, true, true, true, true, true };
    public static bool[] playPoint = { false, false, false, false, false, true, true, true, true, true };
    public static bool isplayed =  false ;
    public static float timer =  1f ;

    // Start is called before the first frame update
    void Start()
    {
        Animation.handler = this;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Interval:
                if(playPoint[DialogBoxHandler.cbnum]){
                    playPoint[DialogBoxHandler.cbnum]=false;
                    GameGlobalController.gameState=GameGlobalController.GameState.Playing;
                }
                else GameGlobalController.gameState=GameGlobalController.GameState.Animation;
                break;
            case GameGlobalController.GameState.Playing:
                if(!isplayed)
                    this.delta+=Time.deltaTime;
                if(!isplayed&&this.delta>=timer)   {
                    this.delta=0;
                    isplayed=true;
                    GameGlobalController.gameState=GameGlobalController.GameState.Interval;
                } 
                break;
        }
    }

    public override void handle()
    {
        if(DialogBoxHandler.cbnum==0){
            isplayed=false;
            for(int i=0;i<playPoint.Length;i++) playPoint[i]=initialPoint[i];
        }
    }

    public override void trigger(int id)
    {
        switch (id)
        {
            case 0:
                if(DialogBoxHandler.cbnum != 9)
                {
                    GameGlobalController.gameState = GameGlobalController.GameState.Animation;
                }
                break;
            case 1:
                GameGlobalController.gameState = GameGlobalController.GameState.Shaking;
                break;
            case 2:
                if(DialogBoxHandler.cbnum == 7)
                {
                    DialogBoxHandler.cbnum = 8;
                    GameGlobalController.gameState = GameGlobalController.GameState.Animation;
                }
                break;
        }
    }
}
