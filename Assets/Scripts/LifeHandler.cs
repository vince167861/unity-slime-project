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
    //public float sufferamount = 0;
    public float tghealamount = 0;
    public float tgsufferamount = 0;
    public static bool isSuffer = false;
    public bool anim1,anim2 = false;
    static Animator animator;
    static Animator animator2;
    Image Bar;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator2 = GameObject.Find("Suffer").GetComponent<Animator>();
        Bar = GameObject.Find("Bar").GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Playing:
                targetlife = lastlife + healamount - sufferchange.sufferamount;
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
                        if(!animator.enabled || !anim1)
                        {
                            anim1 = true;
                            animator.enabled = true;
                            animator.Play("lifeheal",0,0);
                        }
                    }
                }
                if(Input.GetKeyDown(KeyCode.I))
                {
                    tgsufferamount += 30;
                    isSuffer = true;
                    if(!animator2.enabled || !anim2)
                    {
                        anim2 = true;
                        animator2.enabled = true;
                        animator2.Play("lifesuffer2",0,0);
                    }
                }
                if(healamount > tghealamount)
                {
                    animator.enabled = false;
                    healamount = tghealamount;
                    if(sufferchange.sufferamount >= tgsufferamount)
                    {
                        lastlife = targetlife;
                        healamount = 0;
                        tghealamount = 0;
                    }
                    
                }
                if(sufferchange.sufferamount > tgsufferamount)
                {
                    animator2.enabled = false;
                    sufferchange.sufferamount = tgsufferamount;
                    if(healamount >= tghealamount)
                    {
                        lastlife = targetlife;
                        isSuffer = false;
                        tgsufferamount = 0;
                        healamount = 0;
                        tghealamount = 0;
                    }
                }
                break;
        }
    }
}

