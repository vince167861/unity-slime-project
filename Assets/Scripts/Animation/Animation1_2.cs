using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation1_2 : Animation
{
    // Start is called before the first frame update
    void Start()
    {
        Animation.handler = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void handle()
    {}

    public override void trigger(int triggerId)
    {}
}
