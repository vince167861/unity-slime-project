using UnityEngine;
using TMPro;

public class HealthIndicator : MonoBehaviour
{
	Entity parent;
	public Transform filling;
	public TextMeshPro nameText;
	public SpriteRenderer fillingColor;

	private void Start()
	{
		parent = GetComponentInParent<Entity>();
		nameText.text = parent.spriteName;
	}

	private void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.Animation:
			case Game.GameState.Shaking:
			case Game.GameState.Playing:
				if (filling.localScale.x <= 0.02) parent.Die();
				transform.localScale = new Vector3(-1 * parent.entityDirection, 1, 1);
				fillingColor.color = Color.HSVToRGB(0.25f + (parent.healthPercentage - 1) * 0.25f, 1, 1);
				if (filling.localScale.x - parent.healthPercentage > 0.02)
					filling.localScale -= new Vector3(0.01f, 0, 0);
				if (filling.localScale.x - parent.healthPercentage < -0.02)
					filling.localScale += new Vector3(0.01f, 0, 0);
				break;
		}
	}
}
