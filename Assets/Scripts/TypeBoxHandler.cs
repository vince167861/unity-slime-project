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
        
    }

    public void Yes()
    {
        if(!isName)
        {
            if(Game.storystate == 9 || Game.storystate == 0 || Game.isUser)
            {
                LevelVarity.me = Input.text;
                Game.storystate = 0;
                Game.battle = true;
                Game.gameState = Game.GameState.LevelPrepare;
                Game.givename();
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
