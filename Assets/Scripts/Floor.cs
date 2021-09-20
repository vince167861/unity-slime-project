using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject Funis;
    // Start is called before the first frame update
    void Start()
    {
        Funis.SetActive(Game.currentLevel == 3);
    }

    // Update is called once per frame
    void Update()
    {
        switch(Game.gameState)
        {
            case Game.GameState.LevelPrepare:
                Destroy(gameObject);
                break;
        }
    }
}
