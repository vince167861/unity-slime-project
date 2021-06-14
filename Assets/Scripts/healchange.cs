using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healchange : MonoBehaviour
{
    public float healamount2 = 0;
    public static float healamount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LifeHandler.isHeal)
            healamount = healamount2;
        else
        {
            healamount = 0;
            healamount2 = 0;
        }
    }
}
