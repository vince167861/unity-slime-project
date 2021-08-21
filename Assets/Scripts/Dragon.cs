using UnityEngine;

public class Dragon : MonoBehaviour
{
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
