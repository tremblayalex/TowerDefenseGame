using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EnemyScriptableObject[] enemyScriptableObjects;
    public GameObject enemyPrefab;
    public float waveDuration =60f;
    public float delayBetweenWaves = 15f;
    public int firstWaveEnemyCount = 10;
    public float waveEnemyIncrementMultiplier = 1.2f;
    

    private float delayBeforeNextSpawn = 0f;
    private float timeBetweenWaves;
    private int ennemiCount ;
    private int waveNumber = 1;

    void Start()
    {
        timeBetweenWaves = waveDuration + delayBetweenWaves;
        ennemiCount = firstWaveEnemyCount;
    }

    void Update()
    {
      
       
        if (delayBeforeNextSpawn <= 0)
        {
         
            float spawnDelay = waveDuration/ ennemiCount; 
            IEnumerator coroutine = SpawnAWave(ennemiCount, spawnDelay);
            StartCoroutine(coroutine);
            ennemiCount = (int)(ennemiCount * waveEnemyIncrementMultiplier);
            Debug.Log("count :" + ennemiCount);
            delayBeforeNextSpawn = timeBetweenWaves;
        }

        delayBeforeNextSpawn -= Time.deltaTime;
    }

     IEnumerator SpawnAWave(int compteur ,float timeBetwenEnnemi)
     {
     
        for (int i = 0; i < compteur; i++)
        { 

            SpawnNewEnemy();
            yield return new WaitForSeconds(timeBetwenEnnemi);

        }
        waveNumber++;
        
        print("wave is finish");

     }

    private void SpawnNewEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
        newEnemy.GetComponent<Enemy>().setHitPoints(enemyScriptableObjects[0].hitPoints);
        newEnemy.GetComponent<Enemy>().setMovementSpeed(enemyScriptableObjects[0].movementSpeed);
        newEnemy.GetComponent<SpriteRenderer>().sprite = enemyScriptableObjects[0].enemySprite;
    }
}
