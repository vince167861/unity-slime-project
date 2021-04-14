﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdHandler : Entity, Attackable
{
    public BirdHandler() : base(150) { }
    public int AttackDamage { get => 1; }

    float moveSpeed = 0.03f; //bird movement speed
    public float stayRed = 0;
    public float delta2 = 0;
    float offset = 0;
    Animator animator;
    SpriteRenderer sprite;
    /// <summary>
    /// False is to left.
    /// </summary>
    public bool flyingDirection = false;
    bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.flipX = flyingDirection;
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
            case GameGlobalController.GameState.Animation:
                transform.Find("Progress Bar").Find("Fillings").localScale = new Vector3(((float)health) / 150, 1, 1);
                if (stayRed <= 0)
                {
                    if (isDead)
                    {
                        GetComponentInParent<EnemySpawnerHandler>().isActive = true;
                        Destroy(gameObject);
                    }
                    else GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 90);
                } else stayRed -= Time.deltaTime;
                float newY;
                do
                {
                    newY = Random.Range(-0.03f, 0.03f);
                } while (Mathf.Abs(newY + offset) >= 0.6);
                offset += newY;
                transform.Translate(new Vector2(moveSpeed * (flyingDirection ? 1 : -1), newY));
                animator.speed = 1.0f;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Bomb":
                Destroy(col.gameObject);
                GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 90);
                stayRed = 0.2f;
                isDead = !Suffer(col.GetComponent<Attackable>().AttackDamage, true);
                break;
        }
    }


}
