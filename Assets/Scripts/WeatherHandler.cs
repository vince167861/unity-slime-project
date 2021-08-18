using UnityEngine;

public class WeatherHandler : MonoBehaviour
{
	void Update()
	{
		if (Game.storyEffect == Game.StoryEffect.Clear) Destroy(gameObject);
		switch (Game.gameState)
		{
			case Game.GameState.DarkFadeOut:
				Destroy(gameObject);
				break;
		}
	}
}
