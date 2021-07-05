using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCover : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(GetComponent<RectTransform>().anchoredPosition, Slime.transform.position) < 20)
          Destroy(gameObject);
        if (GameGlobalController.isDarking) Destroy(gameObject);
    }
}
