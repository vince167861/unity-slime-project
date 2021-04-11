using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gotoportal : MonoBehaviour
{
    Animator anim;
    bool trigger = false;
    float delta = 3;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.End:
                Destroy(gameObject);
                break;
            case GameGlobalController.GameState.Playing:
            case GameGlobalController.GameState.Lobby:
                delta += Time.deltaTime;
                if (Input.GetKey(KeyCode.G) && trigger)
                {
                    anim.Play("opendoor");
                    delta = 0;
                }
                if (delta >= 0.5f && delta <= 1)
                {
                    trigger = false;
                    if(GameGlobalController.gameState==GameGlobalController.GameState.Playing)
                        GameGlobalController.WinPass();
                    else
                        GameGlobalController.GameReset();
                }
                break;

        }

    }
    void OnTriggerEnter2D(Collider2D col)
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
}
