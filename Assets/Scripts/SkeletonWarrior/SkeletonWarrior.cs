using UnityEngine;

public class SkeletonWarrior : Entity
{
	public SkeletonWarrior() : base("¼B¤j­ô", 500) { }

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
