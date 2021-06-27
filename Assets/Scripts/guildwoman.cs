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
			case GameGlobalController.GameState.Start:
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
					DialogBoxHandler.advice(3, GameGlobalController.currentLevel - 1);
				}
				if (Input.GetKey(KeyCode.G) && trigger1)
				{
					DialogBoxHandler.advice(3, -1);
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
