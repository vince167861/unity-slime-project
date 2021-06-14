using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sufferchange : MonoBehaviour
{
    public float sufferamount2 = 0;
    public static float sufferamount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LifeHandler.isSuffer)
            sufferamount = sufferamount2;
        else
        {
            sufferamount = 0;
            sufferamount2 = 0;
        }
    }
}
