using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attacking()
    {
        this.transform.Find("武士刀").gameObject.GetComponent<ParticleSystem>().Play();
    }

    void noAttack()
    {
        this.transform.Find("武士刀").gameObject.GetComponent<ParticleSystem>().Stop();
    }
}