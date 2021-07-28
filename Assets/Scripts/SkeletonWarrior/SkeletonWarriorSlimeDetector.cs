using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorSlimeDetector : MonoBehaviour
{
	private Animator skeletonAnimator;

	private void Start()
	{
		skeletonAnimator = GetComponentInParent<Animator>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		switch(collision.tag)
		{
			case "Slime":
				skeletonAnimator.Play("©Þ¤M");
				break;
		}
	}
}
