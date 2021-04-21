using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHandler : MonoBehaviour
{
    GameObject towerHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Enemy":
                BirdHandler bh = col.GetComponent<BirdHandler>();
                bh.flyingDirection = bh.flyingDirection != true;
                SpriteRenderer sr = col.GetComponent<SpriteRenderer>();
                sr.flipX = sr.flipX != true;
                break;
            case "Bomb":
                Destroy(col.gameObject);
                break;
        }
    }
}
