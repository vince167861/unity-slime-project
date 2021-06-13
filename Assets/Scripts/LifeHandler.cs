using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public float targetlife = 100;
    public float lastlife = 100;
    public float healamount = 0;
    public float sufferamount = 0;
    public float tghealamount = 0;
    public float tgsufferamount = 0;
    static Animator animator;
    static Animation anim1;
    Image Bar;

    void Start()
    {
        animator = GetComponent<Animator>();
        anim1 = GetComponent<Animation>();
        Bar = GameObject.Find("Bar").GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Playing:
                targetlife = lastlife + healamount - sufferamount;
                Bar.fillAmount = targetlife/100;
                Bar.color = Color.HSVToRGB(0.2f*(targetlife/100),1,1);
                if(Input.GetKeyDown(KeyCode.U))
                {
                    if(targetlife > 70)
                        tghealamount = 100 - targetlife;
                    else  tghealamount +=30;
                    if(tghealamount > 100 - targetlife)  tghealamount = 100 - targetlife;
                    if(tghealamount != 0)  
                    {
                        animator.enabled = true;
                        animator.Play("lifeheal");
                    }
                }
                if(Input.GetKeyDown(KeyCode.I))
                {
                    tgsufferamount += 30;
                    animator.enabled = true;
                    animator.Play("lifesuffer",1,0);
                }
                if(healamount > tghealamount)
                {
                    animator.enabled = false;
                    healamount = tghealamount;
                    if(sufferamount >= tgsufferamount)
                    {
                        lastlife = targetlife;
                        healamount = 0;
                        tghealamount = 0;
                    }
                    
                }
                if(sufferamount > tgsufferamount)
                {
                    animator.enabled = false;
                    sufferamount = tgsufferamount;
                    if(healamount >= tghealamount)
                    {
                        lastlife = targetlife;
                        sufferamount = 0;
                        tgsufferamount = 0;
                    }
                }
                break;
        }
    }
}

