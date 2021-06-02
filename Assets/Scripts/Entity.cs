using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int direction;
    readonly int def;
    readonly Action<Entity> sufferCallback, deathCallback;
    private int __health;
    public bool invulnerable = false;
    public int health => __health;

    static void __default_death_callback(Entity entity) {Destroy(entity.gameObject);}
    public Entity(int h, int d = 1, Action<Entity> scb = null, Action<Entity> dcb = null)
    {
        __health = def = h;
        direction = d;
        sufferCallback = scb;
        deathCallback = dcb ?? __default_death_callback;
    }

    public void Suffer(int damage)
    {
        if (!invulnerable)
        {
            __health -= Math.Abs(damage);
            sufferCallback(this);
            if (__health <= 0) deathCallback(this);
        }
    }

    public void Heal(int amount, bool ignoreMax = false)
    {
        if (__health >= def && !ignoreMax) return;
        __health += Math.Abs(amount);
        if (__health >= def && !ignoreMax) __health = def;
    }

    public void ResetHealth()
    {
        __health = def;
    }
}
