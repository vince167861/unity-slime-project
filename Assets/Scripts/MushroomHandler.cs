using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomHandler : MonoBehaviour
{
    float living = 0;
    public void trigger()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2((GameGlobalController.slimeInstance.transform.position.x - transform.position.x)*300, 40.0f));
        living += Time.deltaTime;
    }
}
