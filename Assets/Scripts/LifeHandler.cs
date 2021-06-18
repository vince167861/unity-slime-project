using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public static float entitylife = 100;
    public static float targetlife = entitylife;
    public static float lastlife = entitylife;
    //public static float healamount = 0;
    //public float sufferamount = 0;
    public static float tghealamount = 0;
    public static float tgsufferamount = 0;
    public static bool isSuffer,isHeal = false;
    public static bool anim1,anim2 = false;
    public static bool start = true;
    static Animator animator;
    static Animator animator2;
    Image Bar;
    Text Life;
    Text Name;

    void Start()
    {
        animator = GameObject.Find("Heal").GetComponent<Animator>();
        animator2 = GameObject.Find("Suffer").GetComponent<Animator>();
        Bar = GameObject.Find("Bar").GetComponent<Image>();
        Life = GameObject.Find("TargetLife").GetComponent<Text>();
        Name = GameObject.Find("CharacterName").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Animation:
            case GameGlobalController.GameState.Shaking:
            case GameGlobalController.GameState.PrepareDialog:
            case GameGlobalController.GameState.Playing:
                if(start)
                {
                    targetlife = entitylife;
                    lastlife = targetlife;
                    tghealamount = 0;
                    tgsufferamount = 0;
                    healchange.healamount = 0;
                    sufferchange.sufferamount = 0;
                    isHeal = false;
                    isSuffer = false;
                    anim1 = false;
                    anim2 = false;
                    start = false;
                }
                if(tghealamount > 30)  animator.speed = tghealamount/30;
                else  animator.speed = 1;
                if(tgsufferamount > 30)  animator2.speed = tgsufferamount/30;
                else animator.speed = 1;
                Name.text = "Slime";
                Life.text = (int)targetlife + " / " + entitylife;
                if(targetlife > 100)
                {
                    lastlife = entitylife;
                    isHeal = false;
                    tghealamount = 0;
                }
                targetlife = lastlife + healchange.healamount - sufferchange.sufferamount;
                Bar.fillAmount = targetlife/entitylife;
                Bar.color = Color.HSVToRGB(0.2f*(targetlife/entitylife),1,1);
                if(Input.GetKeyDown(KeyCode.U))  Heal(30);
                if(Input.GetKeyDown(KeyCode.I))  Suffer(30);
                if(healchange.healamount > tghealamount)
                {
                    animator.enabled = false;
                    healchange.healamount = tghealamount;
                    if(sufferchange.sufferamount >= tgsufferamount)
                    {
                        lastlife = targetlife;
                        isHeal = false;
                        tghealamount = 0;
                    }
                    
                }
                if(sufferchange.sufferamount > tgsufferamount)
                {
                    animator2.enabled = false;
                    sufferchange.sufferamount = tgsufferamount;
                    if(healchange.healamount >= tghealamount)
                    {
                        lastlife = targetlife;
                        isSuffer = false;
                        tgsufferamount = 0;
                        tghealamount = 0;
                    }
                }
                break;
        }
    }
    public static void Heal(float amount)
    {
        Slime.Healanim();
        tghealamount += amount;
        isHeal = true;
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
    public static void Suffer(float amount)
    {
        Slime.ImmuneOn();
        tgsufferamount += amount;
        isSuffer = true;
        if(!animator2.enabled || !anim2)
        {
            anim2 = true;
            animator2.enabled = true;
            animator2.Play("lifesuffer",0,0);
        }
    }
}

