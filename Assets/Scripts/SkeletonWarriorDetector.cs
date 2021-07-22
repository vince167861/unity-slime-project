using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorDetector : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "bullet":
				GetComponentInParent<Animator>().Play("©Þ¤M");
				break;
		}
	}
}
