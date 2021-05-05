using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisBrickHandler : MonoBehaviour
{
    public Sprite blockTexture;
    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.collider.tag)
        {
            case "Slime":
                GetComponent<SpriteRenderer>().sprite = blockTexture;
                break;
        }
    }
}
