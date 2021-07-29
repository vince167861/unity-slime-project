using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDragonController : MonoBehaviour
{
    public static Animator animator;
    GameObject dragonhead;
    public static bool levelstory1 = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dragonhead = GameObject.Find("DragonHead");
    }

    // Update is called once per frame
    void Update()
    {
        if(Game.storystate == 0 && LevelVarity.me == null)
            Destroy(gameObject);
        if(Game.storystate == 3)
        {
            animator.Play("storydragon");
            Game.storystate = 4;
        }
        if(levelstory1)
        {
            animator.Play("level1story");
            levelstory1 = false;
        } 
    }

    void fireshot()
    {
        MainCameraHandler.allSound = 1;
        dragonhead.GetComponent<ParticleSystem>().Play();
        animator.Play("firing");
    }

    void firestop()
    {
        MainCameraHandler.allSound = 1;
        dragonhead.GetComponent<ParticleSystem>().Stop();
    }

    void storyend1()
    {
        if(Game.storystate == 4)  Game.storystate = 5;
        Destroy(gameObject);
    }

    void levelonestory()
    {
        animator.SetFloat("storyspeed", 0);
    }
}
