using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	public int entityDirection;
	public readonly int defaultHealth;
	readonly Action<Entity, float> healCallback, sufferCallback;
	readonly Action<Entity>  deathCallback;
	private float __health;
	private string __name;
	public bool isDied = false;
	public bool invulnerable = false;
	public float health => __health;
	public float healthPercentage => __health / defaultHealth;
	public string spriteName => __name;

	static void __default_death_callback(Entity entity) { Destroy(entity.gameObject); }
	static void __default_callback(Entity entity, float delta) { }

	public Entity(string n, int h, int d = 1, Action<Entity, float> scb = null, Action<Entity> dcb = null, Action<Entity, float> hcb = null)
	{
		__name = n;
		__health = defaultHealth = h;
		entityDirection = d;
		sufferCallback = scb ?? __default_callback;
		deathCallback = dcb ?? __default_death_callback;
		healCallback = hcb ?? __default_callback;
	}
	public void Suffer(int damage)
	{
		if (!invulnerable)
		{
			__health -= Math.Abs(damage);
			sufferCallback(this, damage);
			//if (__health <= 0) deathCallback(this);
		}
	}

	public void Heal(int amount, bool ignoreMax = false)
	{

		if (__health >= defaultHealth && !ignoreMax) return;
		__health += Math.Abs(amount);
		healCallback(this, amount);
		if (__health >= defaultHealth && !ignoreMax) __health = defaultHealth;
	}

	public void Die()
	{
		deathCallback(this);
	}

	public void ResetHealth()
	{
		__health = defaultHealth;
	}
}
