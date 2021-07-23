using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
	public static readonly int MAP_SCALE = 2;

	public static Vector2 bgSize = Vector2.zero, terrainSize = Vector2.zero;
	public GameObject mapCover;
	public Sprite[] terrains;
	public static bool isShown = false;

	private Image image;
	/// <summary> Indicates if map has been updated before. </summary>
	/// <remarks> Default to true to prevent map update at game start. </remarks>
	private bool isUpdated = true;


	void Start()
	{
		image = GetComponent<Image>();
	}

	void Update()
	{
		switch (GameGlobalController.gameState)
		{
			case GameGlobalController.GameState.LevelPrepare:
				if (GameGlobalController.battle) isUpdated = false;
				break;
			case GameGlobalController.GameState.DarkFadeIn:
				if (!isUpdated) UpdateMap();
				break;
		}
	}

	private void UpdateMap()
	{
		isUpdated = true;
		image.sprite = terrains[GameGlobalController.currentLevel];
		terrainSize = new Vector2(image.sprite.bounds.size.x, image.sprite.bounds.size.y);
		terrainSize *= bgSize.x / terrainSize.x;
		bgSize = GameGlobalController.background.bounds.size * MAP_SCALE;
		GetComponent<RectTransform>().sizeDelta = terrainSize * bgSize.x / terrainSize.x;
		for (int i = 0; i < bgSize.x; i += 4 * MAP_SCALE)
			for (int j = 0; j < bgSize.y; j += 4 * MAP_SCALE)
			{
				RectTransform c = Instantiate(mapCover).GetComponent<RectTransform>();
				c.SetParent(transform);
				c.anchoredPosition = new Vector2Int(i, j);
				c.localScale = new Vector3Int(MAP_SCALE, MAP_SCALE, 1);
			}
		MapBorder.UpdateAllMapComponentColor();
	}

	protected void UpdateColor()
	{
		image.color = new Color(1, 1, 1, MapBorder.isShown ? 1 : 0);
	}
}
