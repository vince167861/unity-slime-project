using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction : MonoBehaviour
{
    bool trigger = false;
    public static bool isNews = true;
    static GameObject spot; 
    // Start is called before the first frame update
    void Start()
    {
        spot = GameObject.Find("Mission Spot");
    }

    // Update is called once per frame
    void Update()
    {
        spot.SetActive(isNews);
        switch(Game.gameState)
        {
            case Game.GameState.LevelPrepare:
                Destroy(gameObject);
                break;
            case Game.GameState.Lobby:
                if((Input.GetKey(KeyCode.G) || Slime.talk) && trigger)
                {
                    Slime.talk = false;
                    isNews = false;
                    MainCameraHandler.PlayEntityClip(11);
                    Game.gameState = Game.GameState.LobbyInfo;
                }
                break;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Slime":
				CButtonController.talking = true;
                trigger = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Slime":
				CButtonController.talking = false;
                trigger = false;
                break;
        }
    }

    public void turnback()
    {
        MainCameraHandler.PlayEntityClip(3);
        Game.gameState = Game.GameState.Lobby;
    }
}
