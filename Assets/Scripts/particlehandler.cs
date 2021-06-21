using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlehandler : MonoBehaviour
{
    float delta = 0;
    // Start is called before the first frame update
    void Start()
    {
        delta = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        delta -= Time.deltaTime;
        if(delta <= 0)  Destroy(gameObject);
    }
}
