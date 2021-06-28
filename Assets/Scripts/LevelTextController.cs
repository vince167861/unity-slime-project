using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
                    case GameGlobalController.GameState.Playing:
                        animator.SetInteger("level",GameGlobalController.currentLevel);
                        break;
                    case GameGlobalController.GameState.fadeOut:
                        animator.SetInteger("level",-1);
                        break;
        }
    }
}
