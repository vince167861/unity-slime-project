using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChamberControl : MonoBehaviour
{
	public float dx, dy, dz;
	private bool triggered = false;
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Slime") && Input.GetKeyDown(KeyCode.K))
			triggered = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("ChamberRail"))
			Destroy(gameObject);
	}

	private void Update()
	{
		if (triggered)
		{
			transform.parent.Translate(dx, dy, dz);
			Slime.transform.Translate(dx, dy, dz);
		}
	}
}
