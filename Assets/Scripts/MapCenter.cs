using UnityEngine;
using UnityEngine.UI;

public class MapCenter : MonoBehaviour
{
	private Image image;
	private void Start()
	{
		image = GetComponent<Image>();
	}
	void Update()
  {
    image.color = new Color(1, 1, 1, Map.isShown ? 1 : 0);
  }
}
