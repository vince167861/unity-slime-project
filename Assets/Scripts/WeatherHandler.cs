using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherHandler : MonoBehaviour
{
	void Update()
	{
		if (Game.cleareffect)
		{
			Destroy(gameObject);
		}
		switch (Game.gameState)
		{
			case Game.GameState.DarkFadeOut:
				Destroy(gameObject);
				break;
		}
	}
}
