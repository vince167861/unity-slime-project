using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeBoxHandler : MonoBehaviour
{
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
        if(GameGlobalController.storystate == 9 || GameGlobalController.storystate == 0)
        {
            LevelVarity.me = Input.text;
            GameGlobalController.storystate = 0;
            GameGlobalController.battle = true;
            GameGlobalController.gameState = GameGlobalController.GameState.Start;
        }
    }

    public void No()
    {
        Input.text = "";
    }
}
