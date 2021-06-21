using UnityEngine;
using TMPro;

public class LifeIndicator : MonoBehaviour
{

	Entity parent;
	Transform fillings;
	SpriteRenderer fillColor;
	Transform Name;

	void Start()
	{
		parent = GetComponentInParent<Entity>();
		fillings = transform.Find("Sprite Mask/Fillings");
		fillColor = fillings.GetComponent<SpriteRenderer>();
		Name = transform.Find("Name");
	}

	void Update()
	{
		switch (GameGlobalController.gameState)
		{
			case GameGlobalController.GameState.Animation:
			case GameGlobalController.GameState.Shaking:
			case GameGlobalController.GameState.Playing:
				if(fillings.localScale.x <= 0.02) parent.Die();
				transform.localScale = new Vector3(-1 * parent.direction, 1, 1);
				Name.GetComponent<TextMeshPro>().text = parent.name;
				fillColor.color = Color.HSVToRGB(0.25f + (parent.healthPercentage - 1) * 0.25f, 1, 1);
				if (fillings.localScale.x - parent.healthPercentage > 0.02)
					fillings.localScale -= new Vector3(0.01f, 0, 0);
				if (fillings.localScale.x - parent.healthPercentage < -0.02)
					fillings.localScale += new Vector3(0.01f, 0, 0);
				/*if (LifeHandler.start)
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
				if (targethealth > nexthealth) Suffer(speed);*/
				//LifeBar.fillAmount = targethealth/totalhealth;
				break;
		}
	}
	/*public static void Heal(float amount)
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
		Debug.Log(nexthealth);
		if (amount < 0) speed = (targethealth - nexthealth) / 30;
		else speed = 1;
	}*/
}
