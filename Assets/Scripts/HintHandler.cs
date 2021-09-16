using UnityEngine;

public class HintHandler : MonoBehaviour
{
    void Update()
    {
        switch (Game.gameState)
        {
            case Game.GameState.DarkFadeOut:
                Destroy(gameObject);
                break;
        }
    }
}
