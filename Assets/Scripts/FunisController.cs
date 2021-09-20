using UnityEngine;

public class FunisController : MonoBehaviour
{
    bool trigger = false;
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
        switch(Game.gameState)
        {
            case Game.GameState.Lobby:
                if((Input.GetKey(KeyCode.G) || Slime.talk) && trigger)
                {
                    Slime.talk = false;
					DialogBoxHandler.isChat = true;
					DialogBoxHandler.advice(4, 0);
                }
                break;
        }
    }


    void storyend()
    {
        story = true;
        animator.speed = 0;
    }
    
    void OnTriggerStay2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Slime":
				CButtonController.talking = true;
                trigger = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Slime":
				CButtonController.talking = false;
                trigger = false;
                break;
        }
    }

}
