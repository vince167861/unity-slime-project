using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour , Attackable
{
    public int AttackDamage => 6;
    void OnTriggerEnter2D(Collider2D collider)
    {
        switch(collider.tag)
        {
            case "Slime":
                MainCameraHandler.allSound = 10;
                collider.GetComponent<Entity>().Suffer(AttackDamage);
                break;
        }
    }
}

