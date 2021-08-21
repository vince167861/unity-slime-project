using UnityEngine;

public class SkeletonWarriorBulletDetector : MonoBehaviour
{
	private Animator skeletonAnimator;

	private void Start()
	{
		skeletonAnimator = GetComponentInParent<Animator>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "bullet":
				// TODO: Adjust the timer from detected the bullet to destroy it.
				skeletonAnimator.Play("拔刀");
				break;
		}
	}
}
