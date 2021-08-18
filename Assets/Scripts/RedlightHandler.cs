using UnityEngine;

public class RedlightHandler : MonoBehaviour
{
    public static Animator animator;
    public int chatorder = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void next()
    {
        animator.speed = 0;
        chatorder++;
        Game.storychat = chatorder;
    }

    void lightend()
    {
        Game.storyState = Game.StoryState.State7;
    }
}
