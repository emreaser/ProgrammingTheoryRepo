using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject[] flyingEnemies;

    int spawnEnemyCount;
    int flyingEnemyCount;
   

    float waveInterval;
    float spawnInterval = 1.5f;

    Vector3 spawnPositionLeft = new Vector3(-10f, -3.56f, 0.2f);
    Vector3 spawnPositionRight = new Vector3(10f, -3.56f, -0.2f);

    void Start()
    {
        
    }

    void Update()
    {
        if (!MainManager.Instance.gameOver)
        {
            SpawnEnemies();
        } 
        else 
        { 
            StopAllCoroutines(); 
        }
    }

    void SpawnEnemies()
    {
        if (MainManager.Instance.aliveEnemyCount <= 0)
        {
            spawnEnemyCount = Random.Range(5, 12);
            StartCoroutine(SpawnEnemiesCountdown());
        }
        if (MainManager.Instance.aliveFlyingEnemyCount <= 0)
        {
            flyingEnemyCount = Random.Range(3, 9);
            StartCoroutine(SpawnFlyingEnemiesCountDown());
        }
    }

    

    IEnumerator SpawnEnemiesCountdown()
    {
        Vector3 spawnPos;
        Quaternion rotation;
        int leftright;
        int enemyIndex;

      for (int i = 0; i < spawnEnemyCount; i++)
      {
         MainManager.Instance.aliveEnemyCount++;
         leftright = Random.Range(0, 2);
         enemyIndex = Random.Range(0, enemies.Length);

         if (leftright == 0)
         {
            spawnPos = new Vector3(-10f, enemies[enemyIndex].GetComponent<EnemyBase>().heightPos, 0.2f);
            rotation = new Quaternion(0, 1, 0, 0);
         } 
         else 
         { 
            spawnPos = new Vector3(10f, enemies[enemyIndex].GetComponent<EnemyBase>().heightPos, 0.2f);
            rotation = new Quaternion(0, 0, 0, 0); ;
          }
         
         yield return new WaitForSeconds(spawnInterval);
         Instantiate(enemies[enemyIndex], spawnPos, rotation);
      }
    }

    IEnumerator SpawnFlyingEnemiesCountDown()
    {
        Vector3 spawnPos;
        float randomX;
        int randomIndex = Random.Range(0, flyingEnemies.Length);

        for (int i = 0; i < flyingEnemyCount; i++)
        {
            MainManager.Instance.aliveFlyingEnemyCount++;
            randomX = Random.Range(-8,8);
            spawnPos = new Vector3(randomX, 5.5f, 0);
            yield return new WaitForSeconds(spawnInterval);
            Instantiate(flyingEnemies[randomIndex], spawnPos, flyingEnemies[randomIndex].transform.rotation);
        } 
    }

}
