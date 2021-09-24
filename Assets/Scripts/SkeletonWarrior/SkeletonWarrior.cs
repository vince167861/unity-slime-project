using UnityEngine;

public class SkeletonWarrior : Entity
{
	// TODO: Add Heal and Suffer extends from Entity.
	// TODO: Add movement to Skeleton
	public SkeletonWarrior() : base("S.Kさん", 500) { }
	public bool isAttacking = false;

	void Update()
	{
    transform.Translate(new Vector3(entityDirection * -0.1f, 0, 0));
    transform.localScale = new Vector3(entityDirection, 1, 1);
  }

	void Attacking()
	{
		GetComponentInChildren<ParticleSystem>().Play();
		isAttacking = true;
	}

	void noAttack()
	{
		GetComponentInChildren<ParticleSystem>().Stop();
		isAttacking = false;
	}
}
