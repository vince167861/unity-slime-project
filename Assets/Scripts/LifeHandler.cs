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
                break;
        }
    }
}

