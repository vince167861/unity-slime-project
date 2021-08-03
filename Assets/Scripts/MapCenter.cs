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
		rect.anchoredPosition = Slime.instance.transform.position * Map.MAP_SCALE;
	}

	protected void UpdateColor()
	{
		image.color = new Color(1, 1, 1, MapBorder.isShown ? 1 : 0);
	}
}
