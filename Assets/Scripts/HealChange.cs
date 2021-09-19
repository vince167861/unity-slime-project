using UnityEngine;

public class HealChange : MonoBehaviour
{
	public float healAmount2 = 0;
	public static float healAmount = 0;

	void Update()
	{
		healAmount = healAmount2;
	}
}
