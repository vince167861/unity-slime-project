using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CButtonController : MonoBehaviour
{
    public GameObject AttackB, UpB, DownB, HealB, GoB, TalkB, MoveB;
    public static bool portal, talking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GoB.SetActive(portal);
        TalkB.SetActive(talking);
    }
    public void Attack()
    {
        Slime.attack = true;
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
        Slime.healing = true;
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
}
