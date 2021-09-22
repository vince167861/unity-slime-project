using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject MenuB;
    // Start is called before the first frame update
    void Start()
    {
        MenuB.SetActive(Game.currentLevel != 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
