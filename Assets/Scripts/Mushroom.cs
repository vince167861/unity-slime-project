#pragma warning disable CS0108 // 無法辨認的 #pragma 指示詞
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Entity, Attackable
{
	public Mushroom() : base(1, 0) { }
	public int AttackDamage => 40;
	public float jumpSpan = 0, jumpWait = 0;
	private Animator animator;
	public float multiplier = 0.8f;//0.5f

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		switch (collision.collider.tag)
		{
			case "Ground":
				jumpSpan += Time.deltaTime;
				if (jumpSpan >= jumpWait)
				{
					animator.Play("Jump");
					GetComponent<Rigidbody2D>().AddForce(new Vector3(direction * 80, 250, 0) * multiplier);//direction * 100, 300, 0
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
		}
	}
}
