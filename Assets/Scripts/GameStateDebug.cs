using UnityEngine;
using TMPro;

public class GameStateDebug : MonoBehaviour
{
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = "State: " + Game.gameState.ToString() + ", crlvl: " + Game.currentLevel + ", Story: " + Game.storyState.ToString();
    }
}
