using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorSlimeDetector : MonoBehaviour
{
	private Animator skeletonAnimator;
  SkeletonWarrior parent;

	private void Start()
	{
		skeletonAnimator = GetComponentInParent<Animator>();
    parent = GetComponentInParent<SkeletonWarrior>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		switch(collision.tag)
		{
			case "__bullet":
        // TODO: Adjust the timer from detected slime to attack it.
        skeletonAnimator.Play("拔刀");
				break;
		}
  }
  void OnTriggerExit2D(Collider2D collision)
  {
    switch (collision.tag)
    {
      case "Ground":
        parent.entityDirection *= -1;
        break;
    }
  }
}
