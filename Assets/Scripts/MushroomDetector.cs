using UnityEngine;

public class MushroomDetector : MonoBehaviour
{
	private Mushroom parent;
	
	private void Start()
	{
		parent = GetComponentInParent<Mushroom>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "Slime":
				parent.hasTarget = true;
				break;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		switch(collision.tag)
		{
			case "Slime":
				parent.hasTarget = false;
				break;
		}
	}
}
