using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    float delta=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.delta+=Time.deltaTime;
        if(this.delta>=0.17f) Destroy(gameObject);
    }
}
