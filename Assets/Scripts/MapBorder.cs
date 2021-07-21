using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBorder : MonoBehaviour
{
	private Image image;
	private void Start()
	{
		image = GetComponent<Image>();
	}
	void Update()
	{
		image.color = new Color(0, 0, 0, Map.isShown ? 0.5f : 0);
	}
}
