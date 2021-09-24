using UnityEngine;

public class SkeletonWarriorBulletDetector : MonoBehaviour
{
	Animator skeletonAnimator;
  SkeletonWarrior parent;

  void Start()
	{
		skeletonAnimator = GetComponentInParent<Animator>();
    parent = GetComponentInParent<SkeletonWarrior>();
  }

	void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "bullet":
				// TODO: Adjust the timer from detected the bullet to destroy it.
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
