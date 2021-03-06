using UnityEngine;
using UnityEngine.UI;

public class TypeBoxHandler : MonoBehaviour
{
  static bool isName = false;
  public InputField Inputfield;
  Text Input;

  void Start()
  {
    Input = GameObject.Find("Input").GetComponent<Text>();
  }

  void Update()
  {
    if (Game.currentLevel > 0 && isName)
    {
      Inputfield.Select();
      Inputfield.text = DataStorage.me;
      isName = false;
    }
  }

  public void Yes()
  {
    if (!isName)
    {
      if (Game.storyState == Game.StoryState.State9 || Game.storyState == Game.StoryState.NoStory || Game.debugMode || Game.currentLevel > 0)
      {
        DataStorage.me = Input.text;
        if (Game.currentLevel <= 0)
        {
          Game.storyState = Game.StoryState.NoStory;
          Game.battle = true;
          Game.gameState = Game.GameState.LevelPrepare;
        }
        else
        {
          Game.gameState = Game.GameState.Lobby;
        }
        Game.Rename();
        isName = true;
      }
    }
  }

  public void No()
  {
    Inputfield.Select();
    Inputfield.text = "";
  }
}
