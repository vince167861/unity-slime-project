using UnityEngine;
using UnityEngine.UI;

public class MapCover : MonoBehaviour
{
	private Image image;
	private void Start()
	{
		image = GetComponent<Image>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Vector2.Distance(GetComponent<RectTransform>().anchoredPosition, Slime.transform.position * Map.MAP_SCALE) < 20 * Map.MAP_SCALE)
			Destroy(gameObject);
		if (GameGlobalController.isDarking) Destroy(gameObject);
		image.color = new Color(0, 0, 0, Map.isShown ? 1 : 0);
	}
}
