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
		switch (GameGlobalController.gameState)
		{
			case GameGlobalController.GameState.Playing:
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
		if (doContinuePlaying[DialogBoxHandler.cbnum])
		{
			doContinuePlaying[DialogBoxHandler.cbnum] = false;
			GameGlobalController.SetPlaying();
		}
		else GameGlobalController.SetAnimation();
	}

	public override void trigger(int id)
	{
		switch (id)
		{
			case 0:
				if (DialogBoxHandler.cbnum != 9)
					GameGlobalController.SetAnimation();
				break;
			case 1:
				GameGlobalController.gameState = GameGlobalController.GameState.Shaking;
				break;
			case 2:
				if (DialogBoxHandler.cbnum == 7)
				{
					DialogBoxHandler.cbnum = 8;
					GameGlobalController.SetAnimation();
				}
				break;
		}
	}
}
