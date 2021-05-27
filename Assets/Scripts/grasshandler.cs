using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grasshandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collision2D col)
    {
        switch (col.collider.tag)
        {
            case "Bomb":
                Destroy(col.gameObject);
                break;
        }
    }
}
