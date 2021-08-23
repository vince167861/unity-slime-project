using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation1_2 : Animation
{
	float hasDelayed = 0;
	bool isplayed = false;
	public bool[] doContinuePlaying = { false, false, false, false, false, true, true, true, true, true };
	public float timer = 1f;

	void Start() { Animation.handler = this; }

	void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.Playing:
				if (!isplayed)
					if (hasDelayed >= timer)
					{
						isplayed = true;
						handle();
					}
					else
						hasDelayed += Time.deltaTime;
				break;
		}
	}

	public override void handle()
	{
		if (doContinuePlaying[DialogBoxHandler.dialogID])
		{
			doContinuePlaying[DialogBoxHandler.dialogID] = false;
			Game.SetPlaying();
		}
		else Game.SetDialog();
	}

	public override void trigger(int id)
	{
		switch (id)
		{
			case 0:
				if (DialogBoxHandler.dialogID != 9)
					Game.SetDialog();
				break;
			case 1:
				Game.gameState = Game.GameState.Shaking;
				break;
			case 2:
				if (DialogBoxHandler.dialogID == 7)
				{
					DialogBoxHandler.dialogID = 8;
					Game.SetDialog();
				}
				break;
		}
	}
}
