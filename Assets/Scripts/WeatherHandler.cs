using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherHandler : MonoBehaviour
{
	void Update()
	{
		if (GameGlobalController.cleareffect)
		{
			Destroy(gameObject);
		}
		switch (GameGlobalController.gameState)
		{
			case GameGlobalController.GameState.DarkFadeOut:
				Destroy(gameObject);
				break;
		}
	}
}
