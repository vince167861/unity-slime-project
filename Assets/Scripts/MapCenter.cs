using UnityEngine;
using UnityEngine.UI;

public class MapCenter : MonoBehaviour
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
		if (Game.currentLevel == 2) rect.anchoredPosition = (Slime.instance.transform.position - new Vector3(0, 97, 0)) * Map.MAP_SCALE;
		else rect.anchoredPosition = Slime.instance.transform.position * Map.MAP_SCALE;
	}

	protected void UpdateColor()
	{
		image.color = new Color(1, 1, 1, MapBorder.isShown ? 1 : 0);
	}
}
