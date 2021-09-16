using UnityEngine;

public class FunisController : MonoBehaviour
{
    public static Animator animator;
    public static bool story = false;
    public static bool disappear_f = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(disappear_f) animator.speed = 1;
    }


    void storyend()
    {
        story = true;
        animator.speed = 0;
    }
}
