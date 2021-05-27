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
<<<<<<< HEAD
    void OnTriggerEnter2D(Collision2D col)
=======
    void OnTriggerEnter2D(Collider2D col)
>>>>>>> 8d939a465903f3944c589706557803c0bcb4802a
    {
        switch (col.tag)
        {
            case "Bomb":
                Destroy(col.gameObject);
                break;
        }
    }
}
