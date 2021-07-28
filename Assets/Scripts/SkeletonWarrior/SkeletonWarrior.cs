using UnityEngine;

public class SkeletonWarrior : MonoBehaviour
{
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
