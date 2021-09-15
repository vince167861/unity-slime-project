using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour , IAttackable
{
    public int AttackDamage => 150;
    void OnTriggerEnter2D(Collider2D collider)
    {
        switch(collider.tag)
        {
            case "Slime":
                MainCameraHandler.PlayEntityClip(10);
                Game.lastattack = 2;
                collider.GetComponent<Entity>().Suffer(AttackDamage);
                break;
        }
    }
}

