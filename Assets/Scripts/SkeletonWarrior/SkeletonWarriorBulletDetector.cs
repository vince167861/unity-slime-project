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
			case "Slime":
			case "bullet":
				// TODO: Adjust the timer from detected the bullet to destroy it.
				skeletonAnimator.Play("拔刀");
				break;
      case "Ground":
			case "Chamber":
        parent.entityDirection *= -1;
        break;
		}
  }
}
