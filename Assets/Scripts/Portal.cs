using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Portal : MonoBehaviour
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
        switch (Game.gameState)
        {
            case Game.GameState.DarkFadeOut:
                Destroy(gameObject);
                break;
            case Game.GameState.Playing:
                need.text = " x " + DataStorage.doorKey[Game.currentLevel];
                if (Input.GetKey(KeyCode.G) && trigger)
                {
                    if(Slime.keyCount >= DataStorage.doorKey[Game.currentLevel])
                    {
                        MainCameraHandler.PlayEntityClip(7);
                        animator.Play("opendoor");
                    }
                    else
                    {
                        MainCameraHandler.PlayEntityClip(12);
                        DialogBoxHandler.advice(0,0);
                    }
                }
                if (Anim == true)
                {
                    Anim = false;
                    trigger = false;
                    if(Slime.keyCount > DataStorage.doorKey[Game.currentLevel])  Game.moneycount += 100*(Slime.keyCount - DataStorage.doorKey[Game.currentLevel]);
                    Game.OnLevelComplete();
                }
                break;
        }
    }
    void OnTriggerStay2D(Collider2D col)
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
