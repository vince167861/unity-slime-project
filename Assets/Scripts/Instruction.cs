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
        switch(GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Start:
                Destroy(gameObject);
                break;
            case GameGlobalController.GameState.Lobby:
                if(Input.GetKey(KeyCode.G) && trigger)
                {
                    isNews = false;
                    MainCameraHandler.allSound = 11;
                    GameGlobalController.gameState = GameGlobalController.GameState.LobbyInfo;
                }
                break;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Slime":
                trigger = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Slime":
                trigger = false;
                break;
        }
    }

    public void turnback()
    {
        GameGlobalController.gameState = GameGlobalController.GameState.Lobby;
    }
}
