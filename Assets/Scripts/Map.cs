using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
	public static readonly int MAP_SCALE = 2;

	public static Vector2 bgSize = Vector2.zero, terrainSize = Vector2.zero;
	public GameObject mapCover;
	public Sprite[] terrains;
	public static bool isShown = false;

	static Image image;
	static RectTransform rectTransform;
	static Transform staticTransform;
	static Sprite[] staticTerrain;
	static GameObject staticMapCover;


	void Start()
	{
		image = GetComponent<Image>();
		rectTransform = GetComponent<RectTransform>();
		staticTerrain = terrains;
		staticTransform = transform;
		staticMapCover = mapCover;
	}

	public static void UpdateMap()
	{
		image.sprite = staticTerrain[Game.currentLevel];
		bgSize = Game.background.bounds.size * MAP_SCALE;
		terrainSize = new Vector2(image.sprite.bounds.size.x, image.sprite.bounds.size.y);
		terrainSize *= bgSize.x / terrainSize.x;
		rectTransform.sizeDelta = terrainSize * bgSize.x / terrainSize.x;
		/*for (int i = 0; i < bgSize.x; i += 4 * MAP_SCALE)
			for (int j = 0; j < bgSize.y; j += 4 * MAP_SCALE)
			{
				RectTransform c = Instantiate(staticMapCover).GetComponent<RectTransform>();
				c.SetParent(staticTransform);
				c.anchoredPosition = new Vector2Int(i, j);
				c.localScale = new Vector3Int(MAP_SCALE, MAP_SCALE, 1);
			}*/
		MapBorder.UpdateAllMapComponentColor();
	}

	protected void UpdateColor()
	{
		image.color = new Color(1, 1, 1, MapBorder.isShown ? 1 : 0);
	}
}
