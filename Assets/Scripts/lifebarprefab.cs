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

	/*
	警告		CS0108	'lifebarprefab.name' 會隱藏繼承的成員 'Object.name'。若本意即為要隱藏，請使用 new 關鍵字。
	宣告的變數名稱與基底類型中的變數相同，但未使用 new 關鍵字。此警告是為了通知您應使用 new; 宣告變數的方式就如同宣告中使用了 new。
	*/
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
			case GameGlobalController.GameState.PrepareDialog:
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
