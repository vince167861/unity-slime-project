using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation1_1 : Animation
{
	float hasDelayed = 0;
	bool isplayed = false;
	public bool[] doContinuePlaying = { false, false, false, false, false, true, true, false, false, false, false, true };
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
		if (DialogBoxHandler.cbnum == 10)  TDragonController.animator.SetFloat("storyspeed", 1);
		if (DialogBoxHandler.cbnum == 11)
		{
			Game.PreBrightFadeOut();
			MainCameraHandler.PlayEntityClip(1);
		}
		if (DialogBoxHandler.cbnum == 1 || DialogBoxHandler.cbnum == 8)  DialogBoxHandler.playsurprise = true;
		if (DialogBoxHandler.cbnum == 4)  DialogBoxHandler.playHint = true;
	}
	public override void trigger(int id)
	{
		switch (id)
		{
			case 0: Game.SetAnimation(); break;
			case 1:
				Game.gameState = Game.GameState.Shaking;
				break;
		}
	}
}
