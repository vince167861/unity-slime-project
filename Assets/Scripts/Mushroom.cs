using UnityEngine;

public class Mushroom : Entity, IAttackable
{
	public Mushroom() : base("PiPi", 200, -1, null, OnDie) { }
	public int AttackDamage => 40;
	public float jumpSpan = 0, jumpWait = 0;
	public bool hasTarget = false;
	public GameObject dieEffect, potion, paralysisEffect;

	private Animator animator;
	private Rigidbody2D rigidbody2d;

	private void Start()
	{
		animator = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		switch (collision.collider.tag)
		{
			case "Ground":
				if (jumpSpan >= 0) jumpSpan += Time.deltaTime;
				if (jumpSpan >= jumpWait)
				{
					jumpSpan = -1;
					animator.Play("Jump");
					entityDirection = hasTarget ? Slime.instance.transform.position.x > transform.position.x ? 1 : -1 : Mathf.RoundToInt(Random.value) * -2 + 1;
					Vector3 current = transform.localScale;
					transform.localScale = new Vector3(-entityDirection * System.Math.Abs(current.x), current.y, current.z);
					// TODO: Adjust the force when mushroom moves.
					rigidbody2d.AddForce(new Vector3(entityDirection * 80, 250, 0) * (hasTarget ? 2f: 1f));
				}
				break;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.collider.tag)
		{
			case "Ground":
				jumpSpan = 0;
				jumpWait = Random.Range(0.5f, 1.5f);
				break;
			case "Slime":
				if (Game.isPlaying)
				{
					Instantiate(paralysisEffect, this.transform);
					Game.lastattack = 1;
					collision.collider.GetComponent<Entity>().Suffer(AttackDamage);
					collision.collider.GetComponent<Entity>().ApplyEffect(new EntityEffect(EntityEffect.EntityEffectType.Paralyze, 1));
					Destroy(gameObject);
				}
				break;
		}
	}

	static void OnDie(Entity entity)
	{
		entity.GetComponent<Animator>().Play("die");
		entity.entityDirection = 0;
	}

	void DieAnimationEnd()
	{
		Game.moneycount += 30;
		Game.expcount += 3;
		Instantiate(dieEffect).GetComponent<Transform>().position = this.transform.position;
		if (Random.value <= 0.1) Instantiate(potion);
		Destroy(gameObject);
	}
}
