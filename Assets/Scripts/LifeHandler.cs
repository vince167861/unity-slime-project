using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public float currentlife = 100f;
    public static float targetlife = 100f;
    public float healamount, sufferamount = 0f;
    public float tghealamount = 0f;
    public float tgsufferamount = 0f;
    static Animator animator;
    Image Bar;

    void Start()
    {
        animator = GetComponent<Animator>();
        Bar = GameObject.Find("Bar").GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Playing:
                if(currentlife > 100) currentlife = 100;
                targetlife = currentlife + healamount - sufferamount;
                Bar.fillAmount = targetlife/100;
                Bar.color = Color.HSVToRGB(0.2f*(targetlife/100),1,1);
                if(Input.GetKeyDown(KeyCode.U))
                {
                    animator.SetBool("Heal", true);
                    if(targetlife > 70)
                        tghealamount = 100 - targetlife;
                    else
                    {
                        if(tghealamount > 100 - targetlife) tghealamount = 100 - targetlife;
                        else tghealamount += 30;
                    }
                }
                if(Input.GetKeyDown(KeyCode.I))
                {
                    animator.SetBool("Suffer", true);
                    if(targetlife < 30)
                        tgsufferamount += targetlife;
                    else
                        tgsufferamount += 30;
                }
                if(healamount >= tghealamount)
                {
                    currentlife = targetlife;
                    animator.SetBool("Heal", false);
                    healamount = 0;
                    tghealamount = 0;
                }
                if(sufferamount >= tgsufferamount)
                {
                    currentlife = targetlife;
                    animator.SetBool("Suffer", false);
                    sufferamount = 0;
                    tgsufferamount = 0;
                }
                break;
        }
    }
}

