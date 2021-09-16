using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressDown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isPress()
    {
        Slime.down = true;
    }

    public void Normal()
    {
        if(!Input.GetKey(KeyCode.S))    Slime.down = false;
    }
}
