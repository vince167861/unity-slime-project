using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
  public GameObject background;
  public Sprite[] terrains;
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
    if (GameGlobalController.isPlaying)
    {
      image.enabled = true;
      image.sprite = terrains[GameGlobalController.currentLevel];
      rect.sizeDelta = backgroundRenderer.bounds.size;
      rect.anchoredPosition = Slime.transform.position * -1;
    }
    else
    {
      image.enabled = false;
    }
  }
}
