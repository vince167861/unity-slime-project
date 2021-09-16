using UnityEngine;

public class FiregroundHandler : MonoBehaviour
{
	public AudioSource audiosource;
	void Start()
	{
		if(Game.gameState == Game.GameState.Story)  audiosource.Play();
		GetComponent<Animator>().SetBool("strong", Game.storyState == Game.StoryState.State7);
	}
	void Update()
	{
		if (Game.storyEffect == Game.StoryEffect.Clear)
		{
			audiosource.Stop();
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
