using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int direction;
    readonly int def;
    readonly Func<bool> deathCallback;
    private int _health;
    public int health => _health;
    public Entity(int h, int d = 1, Func<bool> dcb = null)
    {
        _health = def = h;
        direction = d;
        deathCallback = dcb;
    }

    public void Suffer(int damage)
    {
        _health -= Math.Abs(damage);
        if (_health <= 0) if (deathCallback()) Destroy(gameObject);
    }

    public void Heal(int amount, bool ignoreMax = false)
    {
        if (_health >= def && !ignoreMax)
            return;
        _health += Math.Abs(amount);
        if (_health >= def)
            _health = def;
    }

    public void ResetHealth()
    {
        _health = def;
    }
}
