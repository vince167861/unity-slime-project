using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PortalHandler : MonoBehaviour
{
    Animator animator;
    static TextMeshPro need;
    bool trigger = false;
    float delta = 3;
    // Start is called before the first frame update
    void Start()
    {
        need = GameObject.Find("needkey").GetComponent<TextMeshPro>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.End:
                Destroy(gameObject);
                break;
            case GameGlobalController.GameState.Playing:
            case GameGlobalController.GameState.Lobby:
                need.text = " x " + LevelVarity.doorKey[GameGlobalController.currentLevel];
                delta += Time.deltaTime;
                if (Input.GetKey(KeyCode.G) && trigger && (GameGlobalController.slimeInstance.GetComponent<Slime>().keyCount >= LevelVarity.doorKey[GameGlobalController.currentLevel]))
                {
                    MainCameraHandler.allSound = 7;
                    animator.Play("opendoor");
                    delta = 0;
                }
                if (delta >= 0.5f && delta <= 1)
                {
                    trigger = false;
                    if(GameGlobalController.isPlaying)
                        GameGlobalController.GoodEnd();
                    else
                        GameGlobalController.StartNewGame();
                }
                break;

        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Slime":
                trigger = true;
                break;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Slime":
                trigger = false;
                break;
        }
    }
}
