using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "bullet":
                MainCameraHandler.allSound = 6;
                Destroy(col.gameObject);
                break;
        }
    }
}
