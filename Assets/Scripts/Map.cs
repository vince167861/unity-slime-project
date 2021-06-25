using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
  public GameObject background;
  public Sprite[] terrains;
  public GameObject mapCover;
  SpriteRenderer backgroundRenderer;
  RectTransform rect;
  Image image;

  void Start()
  {
    backgroundRenderer = background.GetComponent<SpriteRenderer>();
    rect = GetComponent<RectTransform>();
    image = GetComponent<Image>();
  }

  void Update()
  {
    if (GameGlobalController.isBrightening && GameGlobalController.battle) UpdateMap();
    image.enabled = GameGlobalController.isPlaying;
    rect.anchoredPosition = Slime.transform.position * -1;
  }

  private void UpdateMap()
  {
    image.sprite = terrains[GameGlobalController.currentLevel];
    rect.sizeDelta = backgroundRenderer.bounds.size;
    for (int i = 0; i < backgroundRenderer.bounds.size.x; i+=4)
      for (int j = 0; j < backgroundRenderer.bounds.size.y; j+=4)
      {
        GameObject c = Instantiate(mapCover);
        c.GetComponent<RectTransform>().anchoredPosition = new Vector2Int(i, j);
        c.GetComponent<RectTransform>().SetParent(GetComponent<RectTransform>().parent);
      }
  }
}
