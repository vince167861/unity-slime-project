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
				skeletonAnimator.Play("©Þ¤M");
				break;
		}
	}
}
