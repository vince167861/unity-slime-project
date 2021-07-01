using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDragonController : MonoBehaviour
{
    public static Animator animator;
    GameObject dragonhead;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dragonhead = GameObject.Find("DragonHead");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameGlobalController.storystate == 3)
        {
            animator.Play("storydragon");
            GameGlobalController.storystate = 4;
        }
    }

    void fireshot()
    {
        dragonhead.GetComponent<ParticleSystem>().Play();
        animator.Play("firing");
    }

    void firestop()
    {
        dragonhead.GetComponent<ParticleSystem>().Stop();
    }
    void storyend1()
    {
        GameGlobalController.storystate = 5;
        Destroy(gameObject);
    }
}
