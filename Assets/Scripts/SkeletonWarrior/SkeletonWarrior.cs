using UnityEngine;

public class SkeletonWarrior : Entity
{
	// TODO: Add Heal and Suffer extends from Entity.

	public SkeletonWarrior() : base("Mr.SK", 300, 1, null, SOnDie) { }
	public bool isAttacking = false;
	public Transform healthBar;
	public GameObject dieEffect;
	Vector3 transformOrg;

	void Start()
	{
		transformOrg = transform.localScale;
	}

	void Update()
	{
    transform.Translate(new Vector3(entityDirection * -0.03f, 0, 0));
		transform.localScale = Vector3.Scale(transformOrg, new Vector3(entityDirection, 1, 1));
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
		entity.GetComponent<Animator>().Play("sdie");
		entity.entityDirection = 0;
	}

	void SDieAnimationEnd()
	{
		Game.moneycount += 15;
		Game.expcount += 6;
		Instantiate(dieEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
