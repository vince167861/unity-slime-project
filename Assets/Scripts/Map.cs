using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
	public static Vector2 bgSize = Vector2.zero, terrainSize = Vector2.zero;
	public GameObject background, mapCover, current;
	public Sprite[] terrains;
	SpriteRenderer backgroundRenderer;
	RectTransform rect;
	Image image;
	private bool isUpdated = false;
	public static bool isShown = false;

	public static readonly int MAP_SCALE = 2;

	void Start()
	{
		backgroundRenderer = background.GetComponent<SpriteRenderer>();
		rect = current.GetComponent<RectTransform>();
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
		//image.enabled = GameGlobalController.isPlaying;
		rect.anchoredPosition = Slime.transform.position * MAP_SCALE;
		if (Input.GetKeyDown(KeyCode.Space))
		{
			isShown = !isShown;
		}
		if (isShown && !GameGlobalController.isPlaying)
			isShown = false;
		image.color = new Color(1, 1, 1, isShown ? 1 : 0);
	}

	private void UpdateMap()
	{
		isUpdated = true;
		image.sprite = terrains[GameGlobalController.currentLevel];
		terrainSize = new Vector2(image.sprite.bounds.size.x, image.sprite.bounds.size.y);
		terrainSize *= bgSize.x / terrainSize.x;
		bgSize = backgroundRenderer.bounds.size * MAP_SCALE;
		GetComponent<RectTransform>().sizeDelta = terrainSize * bgSize.x / terrainSize.x;
		for (int i = 0; i < bgSize.x; i += 4 * MAP_SCALE)
			for (int j = 0; j < bgSize.y; j += 4 * MAP_SCALE)
			{
				RectTransform c = Instantiate(mapCover).GetComponent<RectTransform>();
				c.SetParent(transform);
				c.anchoredPosition = new Vector2Int(i, j);
				c.localScale = new Vector3Int(MAP_SCALE, MAP_SCALE, 1);
			}
	}
}
