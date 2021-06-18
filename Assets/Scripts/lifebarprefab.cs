using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class lifebarprefab : MonoBehaviour
{
	static float totalhealth = Bird.health;
	public static float targethealth = totalhealth;
	static float nexthealth = targethealth;
	static float speed = 1;
	public static string name = null;

	TextMeshPro Name;

	// Start is called before the first frame update
	void Start()
	{
		Name = this.transform.Find("Charactername").GetComponent<TextMeshPro>();
	}

	// Update is called once per frame
	void Update()
	{
		Name.text = name;
		switch (GameGlobalController.gameState)
		{
			case GameGlobalController.GameState.Animation:
			case GameGlobalController.GameState.Shaking:
			case GameGlobalController.GameState.Playing:
				if (LifeHandler.start)
				{
					nexthealth = totalhealth;
					targethealth = totalhealth;
				}
				if (targethealth > totalhealth)
				{
					nexthealth = totalhealth;
					targethealth = totalhealth;
				}
				if (targethealth < nexthealth) Heal(speed);
				if (targethealth > nexthealth) Suffer(speed);
				//LifeBar.fillAmount = targethealth/totalhealth;
				break;
		}
	}
	public static void Heal(float amount)
	{
		targethealth += amount;
		if (targethealth > nexthealth) targethealth = nexthealth;
	}
	public static void Suffer(float amount)
	{
		targethealth -= amount;
		if (targethealth < nexthealth) targethealth = nexthealth;
	}
	public static void changeamount(float amount)
	{
		nexthealth += amount;
		if (amount < 0) speed = (targethealth - nexthealth) / 30;
		else speed = 1;
	}
}
