using UnityEngine;

public class dragonController : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        switch (Game.gameState)
        {
            case Game.GameState.BrightFadeIn:
                Destroy(gameObject);
                break;
        }
    }
}
