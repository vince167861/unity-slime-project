using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public static float currentlife, targetlife = 100f;
    public float changeamount = 0f;
    public static float targetamount = 0f;
    public static float changeproperty = 1;
    static Animator animator;

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
                targetlife = currentlife + changeproperty*changeamount;
                GameObject.Find("Bar").GetComponent<Image>().fillAmount = targetlife/100;
                if(Input.GetKey(KeyCode.U))
                {
                    changeproperty = 1;
                    targetamount = 30;
                    animator.Play("lifechange");
                }
                if(Input.GetKey(KeyCode.I))
                {
                    changeproperty = -1;
                    targetamount = 30;
                    animator.Play("lifechange");
                }
                if(changeamount >= targetamount)
                {
                    currentlife = targetlife;
                    animator.speed = 0;
                    changeamount = 0;
                    targetamount = 0;
                }
                break;
        }
    }
}

