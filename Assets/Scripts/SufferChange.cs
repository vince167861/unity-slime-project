using UnityEngine;

public class SufferChange : MonoBehaviour
{
	public float sufferAmount2 = 0;
	public static float sufferAmount = 0;

	private void Update()
	{
		sufferAmount = sufferAmount2;
	}
}
