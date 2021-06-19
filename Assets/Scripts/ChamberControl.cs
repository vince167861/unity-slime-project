using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChamberControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (Input.GetKey(KeyCode.K))
		{
            transform.parent.Translate(0, 1, 0);
		}
	}
}
