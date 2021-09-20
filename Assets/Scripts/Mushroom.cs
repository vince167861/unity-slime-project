﻿using UnityEngine;

public class Mushroom : Entity, IAttackable
{
	public Mushroom() : base("PiPi", 200, -1, null, OnDie) { }
	public int AttackDamage => 40;
	public float jumpSpan = 0, jumpWait = 0;
	public bool hasTarget = false;
	public GameObject dieEffect, potion, paralysisEffect;

	private Animator animator;
	private Rigidbody2D rigidbody2d;

	void Start()
	{
		animator = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		switch (Game.gameState)
		{
			case Game.GameState.Playing:
				rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
				break;
			case Game.GameState.Pause:
				rigidbody2d.bodyType = RigidbodyType2D.Static;
				break;
		}
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		switch (collision.collider.tag)
		{
			case "Ground":
				if (Game.isPlaying)
				{
					if (jumpSpan >= 0) jumpSpan += Time.deltaTime;
					if (jumpSpan >= jumpWait)
					{
						jumpSpan = -1;
						animator.Play("Jump");
						entityDirection = hasTarget ? Slime.instance.transform.position.x > transform.position.x ? 1 : -1 : Mathf.RoundToInt(Random.value) * -2 + 1;
						Vector3 current = transform.localScale;
						transform.localScale = new Vector3(-entityDirection * System.Math.Abs(current.x), current.y, current.z);
						// TODO: Adjust the force when mushroom moves.
						rigidbody2d.AddForce(new Vector3(entityDirection * 80, 250, 0) * (hasTarget ? 2f : 1f));
					}
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
					Instantiate(paralysisEffect, transform.position, Quaternion.identity);
					Game.lastattack = 1;
					collision.collider.GetComponent<Entity>().Suffer(AttackDamage);
					collision.collider.GetComponent<Entity>().ApplyEffect(new EntityEffect(EntityEffect.EntityEffectType.Paralyze, 1));
					Destroy(gameObject);
				}
				break;
			case "Slime Detector":
				if (Game.isPlaying)
				{
					Instantiate(paralysisEffect, transform.position, Quaternion.identity);
					Game.lastattack = 1;
					collision.collider.GetComponentInParent<Entity>().Suffer(AttackDamage);
					collision.collider.GetComponentInParent<Entity>().ApplyEffect(new EntityEffect(EntityEffect.EntityEffectType.Paralyze, 1));
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
		Instantiate(dieEffect, transform.position, Quaternion.identity);
		if (Random.value <= 0.5) Instantiate(potion);
		Destroy(gameObject);
	}
}
