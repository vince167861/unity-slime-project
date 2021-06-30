using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningHandler : MonoBehaviour
{
    float delta = 0;
    public float time = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        delta = time;
    }

    // Update is called once per frame
    void Update()
    {
        delta -= Time.deltaTime;
        if(delta <= 0)
        {
            Destroy(gameObject);
        }
    }
}
