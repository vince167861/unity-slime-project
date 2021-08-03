using UnityEngine;

public class StatusBar : MonoBehaviour
{
	Entity parent;
	public GameObject healthIndicator;

	private void Start()
	{
		parent = GetComponentInParent<Entity>();
		healthIndicator.SetActive(false);
	}

	private void Update()
	{
		if (parent.healthPercentage != 1)
		{
			healthIndicator.SetActive(true);
		}
	}
}
