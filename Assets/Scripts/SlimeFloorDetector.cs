using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFloorDetector : MonoBehaviour
{
    Slime parent;

    void Start()
    {
        parent = transform.parent.GetComponent<Slime>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        switch(col.collider.tag)
        {
            case "Ground":
                parent.isTouchingGround = true;
                break;
            case "Grass":
                parent.isTouchingGround = true;
                break;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        switch(col.collider.tag)
        {
            case "Ground":
                parent.isTouchingGround = false;
                break;
            case "Grass":
                parent.isTouchingGround = false;
                break;
        }
    }
}
