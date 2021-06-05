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
    public bool Anim = false;
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
            case GameGlobalController.GameState.Darking:
                Destroy(gameObject);
                break;
            case GameGlobalController.GameState.Playing:
                need.text = " x " + LevelVarity.doorKey[GameGlobalController.currentLevel];
                if (Input.GetKey(KeyCode.G) && trigger)
                {
                    if(Slime.keyCount >= LevelVarity.doorKey[GameGlobalController.currentLevel])
                    {
                        MainCameraHandler.allSound = 7;
                        animator.Play("opendoor");
                    }
                    else
                    {
                        MainCameraHandler.allSound = 12;
                        DialogBoxHandler.advice(0,0);
                    }
                }
                if (Anim == true)
                {
                    Anim = false;
                    trigger = false;
                    GameGlobalController.GoodEnd();
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
