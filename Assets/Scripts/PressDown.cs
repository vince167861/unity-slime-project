using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressDown : MonoBehaviour
{
    bool is_press = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!is_press)    Slime.down = false;
    }

    public void isPress()
    {
        is_press = true;
        Slime.down = true;
    }

    public void Normal()
    {
        is_press = false;
        Slime.down = false;
    }
}
