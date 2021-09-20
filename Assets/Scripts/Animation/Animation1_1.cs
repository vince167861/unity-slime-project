using UnityEngine;

public class Animation1_1 : Animation
{
	float hasDelayed = 0;
	bool isplayed = false;
	public readonly bool[] doContinuePlaying = { false, false, false, false, false, true, true, false, false, false, false, true };
	readonly float timer = 1f;

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
            			Game.SetDialog();
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
			if(DialogBoxHandler.dialogID == 5)	Game.gameState = Game.GameState.Instruction;
			else	Game.SetPlaying();
		}
		else switch (DialogBoxHandler.dialogID)
			{
				case 1:
				case 8:
					DialogBoxHandler.playsurprise = true;
					break;
				case 4:
					DialogBoxHandler.playHint = true;
					break;
				case 10:
					TDragonController.animator.SetFloat("storyspeed", 1);
					break;
				case 11:
					Game.PreBrightFadeOut();
					MainCameraHandler.PlayEntityClip(1);
					break;
			}
	}
	public override void trigger(int id)
	{
		switch (id)
		{
			case 0: Game.SetDialog(); break;
			case 1:
				Game.gameState = Game.GameState.Shaking;
				break;
		}
	}
}
