using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChamberControl : MonoBehaviour
{
	public float dx, dy, dz;
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("ChamberStopper"))
			Destroy(gameObject);
		if (collision.CompareTag("Slime") && Input.GetKey(KeyCode.K))
		{
            transform.parent.Translate(dx, dy, dz);
			Slime.transform.Translate(dx, dy, dz);
		}
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.J))
		{
			transform.parent.Translate(-dx, -dy, -dz);
			Slime.transform.Translate(-dx, -dy, -dz);
		}
	}
}
