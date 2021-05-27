using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisBrickHandler : MonoBehaviour
{
    public Sprite sprite;
    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.collider.tag)
        {
            case "Slime":
                GetComponent<SpriteRenderer>().sprite = sprite;
                break;    
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Bomb":
                GetComponent<SpriteRenderer>().sprite = sprite;
                Destroy(collision.gameObject);
                break;
        }
    }
}
