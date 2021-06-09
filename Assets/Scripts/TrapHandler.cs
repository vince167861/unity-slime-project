using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHandler : MonoBehaviour , Attackable
{
    public int AttackDamage => 6;
    void OnTriggerEnter2D(Collider2D col) 
    {
        switch(col.tag)
        {
            case "Slime":
                MainCameraHandler.allSound = 10;
                col.GetComponent<Entity>().Suffer(AttackDamage);
                break;
        }
    }
}

