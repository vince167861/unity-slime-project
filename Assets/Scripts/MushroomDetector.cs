using System;
using UnityEngine;

public class MushroomDetector : MonoBehaviour
{
	private Mushroom parent;
	
	private void Start()
	{
		parent = GetComponentInParent<Mushroom>();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "Slime":
				parent.multiplier = 1.0f;
				int a = parent.direction = (Slime.transform.position.x - transform.position.x) > 0 ? 1 : -1;
				Vector3 current = transform.parent.localScale;
				transform.parent.localScale = new Vector3(-a * Math.Abs(current.x), current.y, current.z);
				break;
		}
	}
}
