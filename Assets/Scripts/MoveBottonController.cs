using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBottonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<RectTransform>().anchoredPosition.x > 10)
        {
            Slime.right = true;
            Slime.left = false;
        }
        else if(GetComponent<RectTransform>().anchoredPosition.x < -10)
        {
            Slime.left = true;
            Slime.right = false;
        }
        else
        {
            Slime.left = false;
            Slime.right = false;
        }
    }
}
