using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyicon : MonoBehaviour
{
    public static Animator animator;
    static Text keyicon = null;
    // Start is called before the first frame update
    void Start()
    {
        if(keyicon == null)
            keyicon = GameObject.Find("Keynum").GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        keyicon.text = " X  " + Slime.keyCount + " / " + LevelVarity.keyMax[GameGlobalController.currentLevel];
    }
    
    public static void getK()
    {
        animator.Play("getkey");
    }
}
