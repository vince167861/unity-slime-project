using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFloorDetector : MonoBehaviour
{
    SlimeHandler parent;

    void Start()
    {
        parent = transform.parent.GetComponent<SlimeHandler>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        switch(col.collider.tag)
        {
            case "Ground":
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
        }
    }
}
