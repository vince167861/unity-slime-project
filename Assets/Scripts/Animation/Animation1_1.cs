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
		if (DialogBoxHandler.cbnum == 10)  TDragonController.animator.SetFloat("storyspeed", 1);
		if (DialogBoxHandler.cbnum == 11)
		{
			GameGlobalController.gameState = GameGlobalController.GameState.BrightFadeOut;
			MainCameraHandler.allSound = 1;
		}
		if (DialogBoxHandler.cbnum == 1 || DialogBoxHandler.cbnum == 8)  DialogBoxHandler.playsurprise = true;
	}
	public override void trigger(int id)
	{
		switch (id)
		{
			case 0: GameGlobalController.SetAnimation(); break;
			case 1:
				GameGlobalController.gameState = GameGlobalController.GameState.Shaking;
				break;
		}
	}
}
