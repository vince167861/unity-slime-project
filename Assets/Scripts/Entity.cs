using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Entity : MonoBehaviour
{
    public int direction;
    readonly int def;
    readonly Action<Entity> healCallback, sufferCallback, deathCallback;
    private float __health;
    public bool invulnerable = false;
    public float health => __health;

    static void __default_death_callback(Entity entity) {Destroy(entity.gameObject);}
    static void __default_suffer_callback(Entity entity) { }
    
    public Entity(int h, int d = 1, Action<Entity> scb = null, Action<Entity> dcb = null)
    {
        //__health = def = h;
        direction = d;
        sufferCallback = scb ?? __default_suffer_callback;
        deathCallback = dcb ?? __default_death_callback;
    }
    public void Suffer(int damage)
    {
        if (!invulnerable)
        {
            //lifebarprefab.Suffer(damage);
            //__health -= Math.Abs(damage);
            sufferCallback(this);
            if (__health <= 0) deathCallback(this);
        }
    }

    public void Heal(int amount, bool ignoreMax = false)
    {
        //transform.lifebarprefab.Heal(damage);
        //if (__health >= def && !ignoreMax) return;
        //__health += Math.Abs(amount);
        //if (__health >= def && !ignoreMax) __health = def;
    }

    public void ResetHealth()
    {
        //__health = def;
    }
}
