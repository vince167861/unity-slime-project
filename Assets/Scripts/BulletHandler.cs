using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour, Attackable
{
    public float moveSpeed = 0.1f; //bullet movement speed
    GameObject slime;

    int Attackable.AttackDamage => 50;

    // Start is called before the first frame update
    void Start()
    {
        slime = GameObject.Find("Slime");
        if (moveSpeed < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (moveSpeed > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Lobby:
                Destroy(gameObject);
                break;
            case GameGlobalController.GameState.Playing:
                transform.Translate(moveSpeed, 0, 0);
                break;
        }
    }
}
