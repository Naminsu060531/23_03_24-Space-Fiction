using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] Pos;
    public int SpawnPosValue;
    public GameObject[] Enemy;
    public int EnemyValue;
    public float SpawnDelay;
    public float SpawnDelayMax;

    GameManager gameManager;

    void Update()
    {
        SpawnDelay += Time.deltaTime;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(gameManager.stageValue == 1)
        {
            SpawnDelayMax = 3;
        }
        if (gameManager.stageValue == 2)
        {
            SpawnDelayMax = 2;
        }
        if (gameManager.stageValue == 3)
        {
            SpawnDelayMax = 1;
        }

        if (SpawnDelay > SpawnDelayMax)
        {
            SpawnPosValue = Random.Range(0, Pos.Length);
            EnemyValue = Random.Range(0, Enemy.Length);

            Instantiate(Enemy[EnemyValue], Pos[SpawnPosValue].position, Quaternion.identity);

            print("EnemySpawn");

            SpawnDelay = 0;
        }
    }
}
