using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeBoxHandler : MonoBehaviour
{
    public static bool isName = false;
    public InputField Inputfield;
    Text Input;
    // Start is called before the first frame update
    void Start()
    {
        Input = GameObject.Find("Input").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameGlobalController.currentLevel > 0 && isName)
        {
            Inputfield.Select();
            Inputfield.text = LevelVarity.me;
            isName = false;
        }
    }

    public void Yes()
    {
        if(!isName)
        {
            if(GameGlobalController.storystate == 9 || GameGlobalController.storystate == 0 || GameGlobalController.isUser || GameGlobalController.currentLevel > 0)
            {
                LevelVarity.me = Input.text;
                if(GameGlobalController.currentLevel <= 0)
                {
                    GameGlobalController.storystate = 0;
                    GameGlobalController.battle = true;
                    GameGlobalController.gameState = GameGlobalController.GameState.LevelPrepare;
                }
                else
                {
                    GameGlobalController.gameState = GameGlobalController.GameState.Lobby;
                }
                GameGlobalController.givename();
                isName = true;
            }
        }
    }

    public void No()
    {
        Inputfield.Select();
        Inputfield.text = "";
    }
}
