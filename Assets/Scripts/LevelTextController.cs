using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextController : MonoBehaviour
{
    bool animPlay = true;
    Animator animator;
    Text Level, LevelName;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Level = GameObject.Find("Level").GetComponent<Text>();
        LevelName = GameObject.Find("LevelName").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Level.text = "1-" + (GameGlobalController.currentLevel + 1);
        LevelName.text = LevelVarity.LevelName[0][GameGlobalController.currentLevel];
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Playing:
                if(animPlay)
                {
                    animator.Play("levelText");
                    animPlay = false;
                }
                break;
            case GameGlobalController.GameState.Darking:
                animPlay = true;
                break;
        }
    }
}
