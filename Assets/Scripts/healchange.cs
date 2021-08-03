using UnityEngine;

public class HealChange : MonoBehaviour
{
	public float healAmount2 = 0;
	public static float healAmount = 0;

	void Update()
	{
		if (MainCharacterHealth.isHeal)
			healAmount = healAmount2;
		else
		{
			healAmount = 0;
			healAmount2 = 0;
		}
	}
}
