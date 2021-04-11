using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxHandler : MonoBehaviour
{
    public static int cbnum = 0;
    public Sprite[] ch;
    static Text story;
    static Text teller;
    static Image littlech;

    // Start is called before the first frame update
    void Start()
    {
        story = GameObject.Find("Story").GetComponent<Text>();
        teller = GameObject.Find("Teller").GetComponent<Text>();
        littlech = GameObject.Find("Little Character").GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Animation:
                story.text = LevelVarity.story[GameGlobalController.currentLevel][cbnum];
                teller.text = LevelVarity.teller[GameGlobalController.currentLevel][cbnum];
                littlech.sprite = ch[LevelVarity.littlech[GameGlobalController.currentLevel][cbnum]];
                if (Input.GetMouseButtonDown(0))
                {
                    story.text = "";
                    teller.text = "";
                    cbnum++;
                    GameGlobalController.gameState = GameGlobalController.GameState.Interval;
                    Animation.handler.handle();
                }
                break;
        }
    }
}
