using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	public int direction;
	public readonly int defaultHealth;
	private readonly Action<Entity> healCallback, sufferCallback, deathCallback;
	private float __health;
	static float __amount = 0;
	public bool invulnerable = false;
	public float health => __health;
	public float healthPercentage => __health / defaultHealth;
	public static float amount => __amount;

	static void __default_death_callback(Entity entity) { Destroy(entity.gameObject); }
	static void __default_callback(Entity entity) { }

	public Entity(int h, int d = 1, Action<Entity> scb = null, Action<Entity> dcb = null, Action<Entity> hcb = null)
	{
		__health = defaultHealth = h;
		direction = d;
		sufferCallback = scb ?? __default_callback;
		deathCallback = dcb ?? __default_death_callback;
		healCallback = hcb ?? __default_callback;
	}
	public void Suffer(int damage)
	{
		if (!invulnerable)
		{
			__health -= Math.Abs(damage);
			__amount -= Math.Abs(damage);
			sufferCallback(this);
			if (__health <= 0) deathCallback(this);
		}
	}

	public void Heal(int amount, bool ignoreMax = false)
	{

		if (__health >= defaultHealth && !ignoreMax) return;
		__health += Math.Abs(amount);
		__amount -= Math.Abs(amount);
		healCallback(this);
		if (__health >= defaultHealth && !ignoreMax) __health = defaultHealth;
	}

	public void ResetHealth()
	{
		__health = defaultHealth;
	}
}
