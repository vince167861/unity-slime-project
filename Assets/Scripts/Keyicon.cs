using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyicon : MonoBehaviour
{
    static Text keyicon = null;
    // Start is called before the first frame update
    void Start()
    {
        if(keyicon == null)
            keyicon = GameObject.Find("Keynum").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        keyicon.text = " X  " + GameGlobalController.slimeInstance.GetComponent<Slime>().keyCount + " / " + LevelVarity.keyMax[GameGlobalController.currentLevel];
    }
}
