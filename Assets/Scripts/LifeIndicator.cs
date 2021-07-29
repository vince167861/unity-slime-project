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
		switch (Game.gameState)
		{
			case Game.GameState.Animation:
			case Game.GameState.Shaking:
			case Game.GameState.Playing:
				if (fillings.localScale.x <= 0.02) parent.Die();
				transform.localScale = new Vector3(-1 * parent.direction, 1, 1);
				Name.GetComponent<TextMeshPro>().text = parent.spriteName;
				fillColor.color = Color.HSVToRGB(0.25f + (parent.healthPercentage - 1) * 0.25f, 1, 1);
				if (fillings.localScale.x - parent.healthPercentage > 0.02)
					fillings.localScale -= new Vector3(0.01f, 0, 0);
				if (fillings.localScale.x - parent.healthPercentage < -0.02)
					fillings.localScale += new Vector3(0.01f, 0, 0);
				break;
		}
	}
}
