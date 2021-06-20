using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulleteffect : MonoBehaviour
{
    private Bullet parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "bird":
			case "Mushroom":
            case "Trap":
				parent.GetComponent<ParticleSystem>().Play();
				break;
		}
	}
}
