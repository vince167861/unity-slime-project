using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
	Entity parent;
	GameObject barbox;
	LifeIndicator fillings;
    // Start is called before the first frame update
    void Start()
    {
		parent = GetComponentInParent<Entity>();
		barbox = transform.Find("Life Indicator").gameObject;
		barbox.SetActive(false);
		fillings = GetComponentInChildren<LifeIndicator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.healthPercentage != 1)
		{
			barbox.SetActive(true);
		}
    }
}
