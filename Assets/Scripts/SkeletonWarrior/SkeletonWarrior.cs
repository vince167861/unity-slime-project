using UnityEngine;

public class SkeletonWarrior : Entity
{
	// TODO: Add Heal and Suffer extends from Entity.

	public SkeletonWarrior() : base("S.Kさん", 300, -1, null, SOnDie) { }
	public bool isAttacking = false;
	public Transform healthBar;
	public GameObject dieEffect;

	void Update()
	{
    transform.Translate(new Vector3(entityDirection * -0.1f, 0, 0));
    transform.localScale = healthBar.localScale = new Vector3(entityDirection, 1, 1);
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

	static void SOnDie(Entity entity)
	{
		entity.GetComponent<Animator>().Play("die");
		entity.entityDirection = 0;
	}

	void DieAnimationEnd()
	{
		Game.moneycount += 15;
		Game.expcount += 6;
		Instantiate(dieEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
