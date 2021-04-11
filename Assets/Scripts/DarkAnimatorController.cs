using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkAnimatorController : MonoBehaviour
{
	Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {	
        switch(GameGlobalController.gameState)
		{
		    case GameGlobalController.GameState.Darking:
				GetComponent<SpriteRenderer>().color = Color.HSVToRGB(0,0,0);
		    	anim.Play("black");
        		break;
        	case GameGlobalController.GameState.Brightening:
				GetComponent<SpriteRenderer>().color = Color.HSVToRGB(0,0,0);
    			anim.Play("light");
        		break;
			case GameGlobalController.GameState.Lighting:
				GetComponent<SpriteRenderer>().color = Color.HSVToRGB(0,0,100);
		    	anim.Play("black");
        		break;
			case GameGlobalController.GameState.Unlighting:
				GetComponent<SpriteRenderer>().color = Color.HSVToRGB(0,0,100);
		    	anim.Play("light");
        		break;
        	}
    }
}
