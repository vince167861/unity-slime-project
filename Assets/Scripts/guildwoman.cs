using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guildwoman : MonoBehaviour
{
	bool trigger1 = false;
	public static bool trigger2 = false;
	public static bool startanim = false;
	public static bool otheradvice = false;
	float timer = 0.3f;
	float delta = 0;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.MenuPrepare:
				break;
			case Game.GameState.LevelPrepare:
				Destroy(gameObject);
				break;
			case Game.GameState.Lobby:
				if (startanim)
					this.delta += Time.deltaTime;
				if (startanim && this.delta >= timer)
				{
					this.delta = 0;
					startanim = false;
					if(otheradvice)
					{
						DialogBoxHandler.isChat = false;
						DialogBoxHandler.advice(1, Game.currentLevel - 1);
					}
					trigger2 = true;
				}
				if (trigger2 && !otheradvice)
				{
					DialogBoxHandler.isChat = false;
					DialogBoxHandler.advice(3, Game.currentLevel - 1);
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
