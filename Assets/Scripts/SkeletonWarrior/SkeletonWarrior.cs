using UnityEngine;

public class SkeletonWarrior : Entity
{
	// TODO: Add Heal and Suffer extends from Entity.
	// TODO: Add movement to Skeleton
	public SkeletonWarrior() : base("S.Kさん", 500) { }
	public bool isAttacking = false;

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
