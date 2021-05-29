using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Enemy":
                collision.GetComponent<Entity>().direction *= -1;
                break;
            case "Bomb":
                Destroy(collision.gameObject);
                break;
        }
    }
}
