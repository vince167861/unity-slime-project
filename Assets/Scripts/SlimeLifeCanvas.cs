using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeLifeCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    //static int state = 0;
    public float changeamount = 0f;
    public static float currentlife, targetlife = 100f;
    public static float targetamount = 0f;
    public static float changeproperty = 1;
    //static bool shaking = false;
    //RectTransform rectTransform;
    static Animator animator;

    /*public static void Shake()
    {
        animator.Play("shake");
        state = 100;
        shaking = true;
    }*/

    void Start()
    {
        animator = GetComponent<Animator>();
        //rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (shaking)
        {
            if (state % 4 == 0)
            {
                rectTransform.position += new Vector3(1, 1, 0);
                if (state == 4) shaking = false;
            }
            else if (state % 4 == 3) rectTransform.position += new Vector3(-1, -1, 0);
            else if (state % 4 == 2) rectTransform.position += new Vector3(1, -1, 0);
            else if (state % 4 == 1) rectTransform.position += new Vector3(-1, 1, 0);
            state--;
        }*/
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Playing:
                targetlife = currentlife + changeproperty*changeamount;
                break;
        }
    }
}
