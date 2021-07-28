using UnityEngine;
using UnityEngine.UI;

public class MapBorder : MonoBehaviour
{
	private Image image;
	private static MapBorder instance = null;
	public static bool isShown = false;

	private void Start()
	{
		instance = this;
		image = GetComponent<Image>();
		UpdateAllMapComponentColor();
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && GameGlobalController.isPlaying)
		{
			isShown = !isShown;
			BroadcastMessage("UpdateColor", SendMessageOptions.DontRequireReceiver);
		}
		if (isShown && !GameGlobalController.isPlaying)
		{
			isShown = false;
			BroadcastMessage("UpdateColor", SendMessageOptions.DontRequireReceiver);
		}
	}

	protected void UpdateColor()
	{
		image.color = new Color(0, 0, 0, isShown ? 0.5f : 0);
	}

	public static void UpdateAllMapComponentColor()
	{
		instance.BroadcastMessage("UpdateColor", SendMessageOptions.DontRequireReceiver);
	}
}
