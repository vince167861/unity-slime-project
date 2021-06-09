using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Entity, Attackable
{
    public Mushroom() : base(1) {}
    public int AttackDamage => 4;
    readonly float forceMultiplier = 50.0f;
    float living = 0;

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch(collider.tag)
        {
            case "bullet":
                Destroy(collider.gameObject);
                Suffer(collider.GetComponent<Attackable>().AttackDamage);
                break;
            case "Slime":
                GetComponent<Rigidbody2D>().AddForce((Slime.transform.position - transform.position) * forceMultiplier);
                living += Time.deltaTime;
                break;
        }
    }
}
