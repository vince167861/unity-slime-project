using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int direction;
    int _health;
    public int health { get => _health; }
    int def;
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

    public void Reset()
    {
        _health = def;
    }
}
