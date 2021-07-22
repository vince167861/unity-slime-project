using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guildwoman : MonoBehaviour
{
	bool trigger1 = false;
	public static bool trigger2 = false;
	public static bool startanim = false;
	float timer = 0.3f;
	float delta = 0;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		switch (GameGlobalController.gameState)
		{
			case GameGlobalController.GameState.MenuPrepare:
				break;
			case GameGlobalController.GameState.LevelPrepare:
				Destroy(gameObject);
				break;
			case GameGlobalController.GameState.Lobby:
				if (startanim)
					this.delta += Time.deltaTime;
				if (startanim && this.delta >= timer)
				{
					this.delta = 0;
					startanim = false;
					trigger2 = true;
				}
				if (trigger2)
				{
					DialogBoxHandler.isChat = false;
					DialogBoxHandler.advice(3, GameGlobalController.currentLevel - 1);
				}
				if (Input.GetKey(KeyCode.G) && trigger1)
				{
					DialogBoxHandler.isChat = true;
					DialogBoxHandler.advice(3, Random.Range(0, LevelVarity.chat[3].Count));
				}
				break;
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		switch (col.tag)
		{
			case "Slime":
				trigger1 = true;
				break;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		switch (col.tag)
		{
			case "Slime":
				trigger1 = false;
				break;
		}
	}

}
