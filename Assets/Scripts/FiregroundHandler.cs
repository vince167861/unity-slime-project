using UnityEngine;

public class FiregroundHandler : MonoBehaviour
{
	void Start()
	{
		GetComponent<Animator>().SetBool("strong", Game.storyState == Game.StoryState.State7);
	}
}
