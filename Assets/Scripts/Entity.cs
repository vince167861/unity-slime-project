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
    private float targethealth;
    private float nexthealth;
    private float delta = 0;
    private float speed = 1;
    public Image LifeBar;

    static void __default_death_callback(Entity entity) {Destroy(entity.gameObject);}
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Animation:
            case GameGlobalController.GameState.Shaking:
            case GameGlobalController.GameState.Interval:
            case GameGlobalController.GameState.Playing:
                if(LifeHandler.start)
                {
                    nexthealth = __health;
                    targethealth = __health;
                    delta = 0;
                }
                if(targethealth > 100)
                {
                    nexthealth = __health;
                    targethealth = __health;
                }
                delta += Time.deltaTime;
                if(delta >= 3 && GameGlobalController.gameState == GameGlobalController.GameState.Playing && targethealth < 100)  changeamount(30);
                if(targethealth < nexthealth) Heal(speed);
                if(targethealth > nexthealth)
                {
                    if(nexthealth < 0)
                    {
                        LifeHandler.Suffer(-1/1.5f * nexthealth);
                        nexthealth = 0;
                    }
                    else  Suffer(speed);
                }
                LifeBar.fillAmount = targethealth/__health;
                break;
        }
    }
    public Entity(int h, int d = 1, Action<Entity> scb = null, Action<Entity> dcb = null, Image Bar = null)
    {
        LifeBar = Bar;
        __health = def = h;
        direction = d;
        sufferCallback = scb;
        deathCallback = dcb ?? __default_death_callback;
        targethealth = __health;
        nexthealth = __health;
    }
    public void Heal(float amount)
    {
        targethealth += amount;
        if(targethealth > nexthealth)  targethealth = nexthealth;
    }
    public void Suffer(float amount)
    {
        sufferCallback(this);
        targethealth -= amount;
        if(targethealth < nexthealth)  targethealth = nexthealth;
    }
    public void changeamount(float amount)
    {
        nexthealth += amount;
        if(amount < 0)
        {
            speed = (targethealth - nexthealth)/30;
            delta = 0;
        }
        else  speed = 1;
    }
    /*public void Suffer(int damage)
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
    }*/

    public void ResetHealth()
    {
        __health = def;
    }
}
