using UnityEngine;

public class SufferChange : MonoBehaviour
{
	public float sufferAmount2 = 0;
	public static float sufferAmount = 0;

	private void Update()
	{
		if (MainCharacterHealth.isSuffer)
			sufferAmount = sufferAmount2;
		else
		{
			sufferAmount = 0;
			sufferAmount2 = 0;
		}
	}
}
