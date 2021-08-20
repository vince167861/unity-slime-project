using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextController : MonoBehaviour
{
    bool animPlay = true;
    Animator animator;
    Text Level = null, LevelName = null;

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
        Level.text = "1-" + (Game.currentLevel + 1);
        LevelName.text = DataStorage.LevelName[0][Game.currentLevel];
        switch (Game.gameState)
        {
            case Game.GameState.Playing:
                if(animPlay)
                {
                    animator.Play("levelText");
                    animPlay = false;
                }
                break;
            case Game.GameState.DarkFadeOut:
                animPlay = true;
                break;
        }
    }
}
