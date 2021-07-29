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
		if (doContinuePlaying[DialogBoxHandler.cbnum])
		{
			doContinuePlaying[DialogBoxHandler.cbnum] = false;
			Game.SetPlaying();
		}
		else Game.SetAnimation();
	}

	public override void trigger(int id)
	{
		switch (id)
		{
			case 0:
				if (DialogBoxHandler.cbnum != 9)
					Game.SetAnimation();
				break;
			case 1:
				Game.gameState = Game.GameState.Shaking;
				break;
			case 2:
				if (DialogBoxHandler.cbnum == 7)
				{
					DialogBoxHandler.cbnum = 8;
					Game.SetAnimation();
				}
				break;
		}
	}
}
