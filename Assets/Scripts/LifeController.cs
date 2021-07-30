using UnityEngine;

public class LifeController : MonoBehaviour
{
	Entity parent;
	GameObject barbox;
	LifeIndicator fillings;

	void Start()
	{
		parent = GetComponentInParent<Entity>();
		barbox = transform.Find("Life Indicator").gameObject;
		barbox.SetActive(false);
		fillings = GetComponentInChildren<LifeIndicator>();
	}

	void Update()
	{
		if (parent.healthPercentage != 1)
		{
			barbox.SetActive(true);
		}
	}
}
