using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class potionicon : MonoBehaviour
{
    static Text poicon = null;
    // Start is called before the first frame update
    void Start()
    {
        if(poicon == null)
            poicon = GameObject.Find("Potionnum").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        poicon.text = " X  " + Slime.potionCount + " / " + Slime.potionMax;
    }
}
