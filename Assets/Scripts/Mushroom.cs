using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Entity, Attackable
{
    public Mushroom() : base(1, 0) {}
    public int AttackDamage => 40;
    public float jumpSpan = 0, jumpWait = 0;

    void OnTriggerStay2D(Collider2D collider)
    {
        switch(collider.tag)
        {
            case "Slime":
                direction = (Slime.transform.position.x - transform.position.x) > 0 ? 1 : -1;
                break;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        switch(collision.collider.tag)
        {
            case "Ground":
                jumpSpan += Time.deltaTime;
                if (jumpSpan >= jumpWait)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector3(direction * 100, 300, 0));
                }
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Ground":
                jumpSpan = 0;
                jumpWait = Random.Range(0.5f, 1.5f);
                break;
        }
    }
}
