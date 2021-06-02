using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFloorDetector : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D col)
    {
        switch(col.collider.tag)
        {
            case "Ground":
                Slime.isTouchingGround = true;
                break;
            case "Grass":
                Slime.isTouchingGround = true;
                break;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        switch(col.collider.tag)
        {
            case "Ground":
                Slime.isTouchingGround = false;
                break;
            case "Grass":
                Slime.isTouchingGround = false;
                break;
        }
    }
}
