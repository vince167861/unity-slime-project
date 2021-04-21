using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerHandler : MonoBehaviour
{
    public GameObject enemyPrefab;

    float timeFromLastSpawn = 0;
    float timeBetweenSpawn = 2f;
    float timeBetweenSpawn2 = 5f;
    public int flyingDirection = -1;
    public bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameGlobalController.gameState)
        {
            case GameGlobalController.GameState.Playing:
                if (isActive)
                {
                    timeFromLastSpawn += Time.deltaTime;
                    if (timeFromLastSpawn >= Random.Range(timeBetweenSpawn,timeBetweenSpawn2))
                    {
                        GameObject enemy = Instantiate(enemyPrefab);
                        enemy.GetComponent<Transform>().SetParent(GetComponent<Transform>());
                        enemy.GetComponent<Transform>().position = transform.position;
                        enemy.GetComponent<BirdHandler>().flyingDirection = flyingDirection;
                        timeFromLastSpawn = 0;
                        isActive = false;
                    }
                }
                break;
            case GameGlobalController.GameState.End:
                Destroy(gameObject);
                break;
        }
    }
}
