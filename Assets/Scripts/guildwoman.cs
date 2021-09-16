using UnityEngine;

public class GuildWoman : MonoBehaviour
{
	bool touchedWoman = false;
	public static bool trigger2 = false, startanim = false, otheradvice = false;
	readonly float timer = 0.3f;
	float delta = 0;

	void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.LevelPrepare:
				Destroy(gameObject);
				break;
			case Game.GameState.Lobby:
				if (startanim)
				{
					delta += Time.deltaTime;
					if (delta >= timer)
					{
						delta = 0;
						startanim = false;
						if (otheradvice)
						{
							DialogBoxHandler.isChat = false;
							DialogBoxHandler.advice(1, Game.currentLevel - 1);
						}
						trigger2 = true;
					}
				}
				if (trigger2 && !otheradvice)
				{
					DialogBoxHandler.isChat = false;
					DialogBoxHandler.advice(3, Game.currentLevel - 1);
				}
				if (Input.GetKey(KeyCode.G) && touchedWoman)
				{
					DialogBoxHandler.isChat = true;
					DialogBoxHandler.advice(3, Random.Range(0, DataStorage.chat[3].Count));
				}
				break;
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		switch (col.tag)
		{
			case "Slime":
				CButtonController.talking = true;
				touchedWoman = true;
				break;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		switch (col.tag)
		{
			case "Slime":
				CButtonController.talking = false;
				touchedWoman = false;
				break;
		}
	}

}
