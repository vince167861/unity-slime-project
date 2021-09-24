using UnityEngine;

public class Katana : MonoBehaviour
{
	private SkeletonWarrior parent;

	private void Start()
	{
		parent = GetComponentInParent<SkeletonWarrior>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (parent.isAttacking)
			switch (collision.tag)
			{
				case "bullet":
					Destroy(collision.gameObject);
					break;
				case "Slime":
          collision.gameObject.GetComponentInParent<Entity>().Suffer(20);
          break;
			}
	}
}
