using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(Game.gameState)
        {
            case Game.GameState.DarkFadeOut:
                Destroy(gameObject);
                break;
        }
    }
}
