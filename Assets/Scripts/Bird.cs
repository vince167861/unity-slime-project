using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bird : Entity, Attackable
{
    public Bird() : base(150, -1) { }
    public int AttackDamage { get => 1; }

    float moveSpeed = 0.03f; //bird movement speed
    public float immuneTime = 0, delta2 = 0;
    float offset = 0;
    bool isDead = false;

    GameObject progressBar;
    Transform fillings;
    SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        progressBar = transform.Find("Progress Bar").gameObject;
        progressBar.SetActive(false);
        fillings = transform.Find("Progress Bar").Find("Fillings");
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(-direction * 0.5f, 0.5f);
        if (health != 150)
        {
            progressBar.SetActive(true);
            progressBar.transform.localScale = new Vector3(-direction, 1, 1);
            fillings.localScale = new Vector3(((float)health) / 150, 1, 1);
        }
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Lobby:
                Destroy(gameObject);
                break;
            case GameGlobalController.GameState.Playing:
                if (immuneTime <= 0)
                {
                    if (isDead)
                    {
                        GetComponentInParent<EnemySpawnerHandler>().isActive = true;
                        Destroy(gameObject);
                    }
                    else sprite.color = new Color(255, 255, 255, 90);
                } else immuneTime -= Time.deltaTime;
                float newY;
                do newY = Random.Range(-0.03f, 0.03f); while (Mathf.Abs(newY + offset) >= 0.6);
                offset += newY;
                transform.Translate(new Vector2(moveSpeed * direction, newY));
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Bomb":
                if (immuneTime <= 0)
                {
                    Destroy(collision.gameObject);
                    sprite.color = new Color(255, 0, 0, 90);
                    immuneTime = 0.2f;
                    isDead = !Suffer(collision.GetComponent<Attackable>().AttackDamage, true);
                }
                break;
                
        }
    }


}
