using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        GetComponent<RectTransform>().anchoredPosition = Vector2.Scale(new Vector2(-0.37f, -0.37f), Slime.transform.position);
    }
}
