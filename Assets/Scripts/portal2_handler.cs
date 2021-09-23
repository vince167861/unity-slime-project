using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal2_handler : MonoBehaviour
{
	public GameObject key;
	Animator animator;
	bool trigger = false;
	public bool Anim2 = false;
	public int key = 0;
	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
		key.SetActive(key != 0);
	}

	// Update is called once per frame
	void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.DarkFadeOut:
				Destroy(gameObject);
				break;
			case Game.GameState.Lobby:
				if ((Input.GetKey(KeyCode.G) || Slime.go) && trigger)
				{
					Slime.go = false;
					if(Game.currentLevel == 3)
					{
						MainCameraHandler.PlayEntityClip(12);
						Game.gameState = Game.GameState.LobbyInfo;
						Game.is_end = true;
					}
					else if (!Instruction.isNews)
					{
						animator.Play("gotoportal");
						MainCameraHandler.PlayEntityClip(8);
						Slime.instance.disappear();
					}
					else
					{
						MainCameraHandler.PlayEntityClip(12);
						DialogBoxHandler.advice(0, 1);
					}
				}
				if (Anim2 == true)
				{
					Anim2 = false;
					trigger = false;
					Game.StartNewLevel();
				}
				break;
			case Game.GameState.Playing:
        		if ((Input.GetKey(KeyCode.G) || Slime.go) && trigger)
				{
					Slime.go = false;
                    if(Slime.keyCount >= key)
                    {
                        MainCameraHandler.PlayEntityClip(7);
						animator.Play("gotoportal");
						GetComponent<IPortalHandler>().Handle();     //  --> 回去原本位置的程式你寫,記得要分另外一個portal
                    }
                    else	MainCameraHandler.PlayEntityClip(12);
				}
        		break;
    }

	}
	void OnTriggerStay2D(Collider2D col)
	{
		switch (col.tag)
		{
			case "Slime":
				CButtonController.portal = true;
				trigger = true;
				break;
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		switch (col.tag)
		{
			case "Slime":
				CButtonController.portal = false;
				trigger = false;
				break;
		}
	}
}
