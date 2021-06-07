using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class potionicon : MonoBehaviour
{
    public static Animator animator;
    static Text poicon = null;
    // Start is called before the first frame update
    void Start()
    {
        if(poicon == null)
            poicon = GameObject.Find("Potionnum").GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        poicon.text = " X  " + Slime.potionCount + " / " + Slime.potionMax;
    }

    public static void getP()
    {
        animator.Play("getpotion");
    }
}
