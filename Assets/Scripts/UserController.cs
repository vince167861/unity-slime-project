using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
    Text Input;
    // Start is called before the first frame update
    void Start()
    {
        Input = GameObject.Find("Input(U)").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UserCheck()
    {
        if(Input.text == "BrianGodd" || Input.text == "Vince")
            Game.isUser = true;
    }
}
