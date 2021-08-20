using UnityEngine;

public class FunisController : MonoBehaviour
{
    public static Animator animator;
    public static bool story = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void storyend()
    {
        story = true;
    }
}
