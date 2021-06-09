using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "bird":
                collision.GetComponent<Entity>().direction *= -1;
                break;
            case "bullet":
                Destroy(collision.gameObject);
                break;
        }
    }
}
