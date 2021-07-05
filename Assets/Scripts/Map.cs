using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
	Vector2 bgSize = Vector2.zero;
	public GameObject background, mapCover, current;
	public Sprite[] terrains;
	SpriteRenderer backgroundRenderer;
	RectTransform rect;
	Image image;
	private bool isUpdated = false;

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
			case GameGlobalController.GameState.fadeIn:
				if (!isUpdated) UpdateMap();
				break;
		}
		image.enabled = GameGlobalController.isPlaying;
		rect.anchoredPosition = Slime.transform.position;
	}

	private void UpdateMap()
	{
		isUpdated = true;
		image.sprite = terrains[GameGlobalController.currentLevel];
		Vector2 terrainSize = new Vector2(image.sprite.bounds.size.x, image.sprite.bounds.size.y);
		bgSize = backgroundRenderer.bounds.size;
		GetComponent<RectTransform>().sizeDelta = terrainSize * bgSize.x / terrainSize.x;
		for (int i = 0; i < bgSize.x; i += 4)
			for (int j = 0; j < bgSize.y; j += 4)
			{
				RectTransform c = Instantiate(mapCover).GetComponent<RectTransform>();
				c.SetParent(transform);
				c.anchoredPosition = new Vector2Int(i, j);
				c.localScale = new Vector3Int(1, 1, 1);
			}
	}
}
