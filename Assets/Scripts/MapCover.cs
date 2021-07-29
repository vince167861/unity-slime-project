using UnityEngine;
using UnityEngine.UI;

public class MapCover : MonoBehaviour
{
	private Image image;
	private RectTransform rect;

	private void Start()
	{
		image = GetComponent<Image>();
		rect = GetComponent<RectTransform>();
	}

	private void Update()
	{
		if (Vector2.Distance(rect.anchoredPosition, Slime.transform.position * Map.MAP_SCALE) < 20 * Map.MAP_SCALE)
			Destroy(gameObject);
		if (Game.isDarking)
			Destroy(gameObject);
	}

	protected void UpdateColor()
	{
		(image != null ? image : GetComponent<Image>()).color = new Color(0, 0, 0, MapBorder.isShown ? 1 : 0);
	}
}
