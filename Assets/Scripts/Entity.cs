using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int direction;
    readonly int def;
    readonly Func<Entity, bool> sufferCallback, deathCallback;
    private int _health;
    public bool invulnerable = false;
    public int health => _health;
    public Entity(int h, int d = 1, Func<Entity, bool> scb = null, Func<Entity, bool> dcb = null)
    {
        _health = def = h;
        direction = d;
        sufferCallback = scb;
        deathCallback = dcb;
    }

    public void Suffer(int damage)
    {
        if (!invulnerable)
        {
            _health -= Math.Abs(damage);
            sufferCallback(this);
            if (_health <= 0) if (deathCallback(this)) Destroy(gameObject);
        }
    }

    public void Heal(int amount, bool ignoreMax = false)
    {
        if (_health >= def && !ignoreMax) return;
        _health += Math.Abs(amount);
        if (_health >= def && !ignoreMax) _health = def;
    }

    public void ResetHealth()
    {
        _health = def;
    }
}
