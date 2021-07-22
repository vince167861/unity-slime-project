using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "bullet":
				Destroy(collision.gameObject);
				break;
		}
	}
}
