using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
	public float x = 0, y = 0;
	public GameObject background, mapCover;
	public Sprite[] terrains;
	SpriteRenderer backgroundRenderer;
	RectTransform rect;
	Image image;
	List<RectTransform> childrenList;

	void Start()
	{
		backgroundRenderer = background.GetComponent<SpriteRenderer>();
		rect = GetComponent<RectTransform>();
		image = GetComponent<Image>();
		childrenList = new List<RectTransform>();
	}

	void Update()
	{
		if (GameGlobalController.isStart && GameGlobalController.battle) UpdateMap();
		image.enabled = GameGlobalController.isPlaying;
		rect.anchoredPosition = Slime.transform.position * -1;
		int xtarget = Mathf.FloorToInt(-rect.anchoredPosition.x / 4), ytarget = Mathf.FloorToInt(-rect.anchoredPosition.y / 4);
		for (int i = xtarget - 5; i < xtarget + 10; i++)
		{
			for (int j = ytarget - 5; j < ytarget + 10; j++)
			{
				int currentTarget = i * Mathf.CeilToInt(y / 4) + j;
				if (currentTarget >= 0 && currentTarget < childrenList.Count && childrenList[currentTarget] != null)
				{
					Destroy(childrenList[currentTarget].gameObject);
					childrenList[currentTarget] = null;
				}
			}
		}
		
	}

	private void UpdateMap()
	{
		image.sprite = terrains[GameGlobalController.currentLevel];
		rect.sizeDelta = backgroundRenderer.bounds.size;
		x = backgroundRenderer.bounds.size.x;
		y = backgroundRenderer.bounds.size.y;
		for (int i = 0; i < x; i += 4)
			for (int j = 0; j < y; j += 4)
			{
				RectTransform c = Instantiate(mapCover).GetComponent<RectTransform>();
				c.SetParent(rect);
				c.anchoredPosition = new Vector2Int(i, j);
				c.localScale = new Vector3Int(1, 1, 1);
				childrenList.Add(c);
			}
	}
}
