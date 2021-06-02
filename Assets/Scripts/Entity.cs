using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int direction;
    readonly int def;
    private int _health;
    public int health => _health;
    public Entity(int h, int d = 1)
    {
        _health = def = h;
        direction = d;
    }

    public bool Suffer(int damage, bool ignoreMin = false)
    {
        _health -= Math.Abs(damage);
        if (_health <= 0)
        {
            if (ignoreMin) _health = 0; else Destroy(gameObject);
            return false;
        }
        return true;
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
