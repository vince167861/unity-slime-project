using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiregroundHandler : MonoBehaviour
{
    public static Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if(Game.storystate == 7)  animator.SetBool("strong", true);
        else  animator.SetBool("strong", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
