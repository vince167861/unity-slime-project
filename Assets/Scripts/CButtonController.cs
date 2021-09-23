using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CButtonController : MonoBehaviour
{
    public GameObject AttackB, UpB, DownB, HealB, GoB, TalkB, MoveB, Map;
    public static bool portal, talking = false;
    
    void Update()
    {
        GoB.SetActive(portal);
        TalkB.SetActive(talking);
        Map.SetActive(Game.gameState == Game.GameState.Playing);
    }
    public void Attack()
    {
        if(Game.gameState == Game.GameState.Playing)    Slime.attack = true;
    }
    public void Up()
    {
        Slime.up = true;
    }
    public void Down()
    {
        Slime.down = true;
    }
    public void Heal()
    {
        if(Game.gameState == Game.GameState.Playing)    Slime.healing = true;
    }
    public void GetIn()
    {
        Slime.go = true;
    }
    public void Talk()
    {
        Slime.talk = true;
    }
    public void LeftMove()
    {
        Slime.left = true;
    }
    public void RightMove()
    {
        Slime.right = true;
    }
    public void Pose()
    {
        Slime.pose = true;
    }
    public void MapOpen()
    {
        if(Game.gameState == Game.GameState.Playing)    Slime.map = true;
    }
}
