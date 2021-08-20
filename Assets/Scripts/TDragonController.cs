using UnityEngine;

public class TDragonController : MonoBehaviour
{
    public static Animator animator;
    GameObject dragonhead;
    public static bool levelstory1 = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        dragonhead = GameObject.Find("DragonHead");
    }

    void Update()
    {
        if (Game.storyState == Game.StoryState.NoStory && DataStorage.me == null)
            Destroy(gameObject);
        if (Game.storyState == Game.StoryState.StoryDragon)
        {
            animator.Play("storydragon");
            Game.storyState = Game.StoryState.DragonShow;
        }
        if (levelstory1)
        {
            animator.Play("level1story");
            levelstory1 = false;
        } 
    }

    void fireshot()
    {
        MainCameraHandler.PlayEntityClip(1);
        dragonhead.GetComponent<ParticleSystem>().Play();
        animator.Play("firing");
    }

    void firestop()
    {
        MainCameraHandler.PlayEntityClip(1);
        dragonhead.GetComponent<ParticleSystem>().Stop();
    }

    void storyend1()
    {
        if (Game.storyState == Game.StoryState.DragonShow)
            Game.storyState = Game.StoryState.House;
        Destroy(gameObject);
    }

    void levelonestory()
    {
        animator.SetFloat("storyspeed", 0);
    }
}
