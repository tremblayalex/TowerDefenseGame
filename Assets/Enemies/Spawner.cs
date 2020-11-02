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
    public int firstWaveEnemyCount = 3;
    public float waveEnemyIncrementMultiplier = 2f;
    

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
     
     IEnumerator SpawnAWave(int nombreEnnemie, float timeBetwenEnnemi)
     {

        //for (int i = 0; i < compteur; i++)     

        while (nombreEnnemie <= 0)
        {
            CustomWave(ref nombreEnnemie);
            
            yield return new WaitForSeconds(timeBetwenEnnemi);

        }
        waveNumber++;

        print("wave is finish");

     }

    private void CustomWave(ref int nombreEnnemi)
    {
        if (nombreEnnemi > (ennemiCount/2))
        {

        }
        SpawnNewEnemy(1);
        nombreEnnemi--;
    }

    private void SpawnNewEnemy(int indexEnnemi)
    {
        //generer random entre 0 et numero wave -1
        //ensuite chercher le scriptable object 
        GameObject newEnemy = Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
        newEnemy.GetComponent<Enemy>().setHitPoints(enemyScriptableObjects[indexEnnemi].hitPoints);

        newEnemy.GetComponent<Enemy>().setMovementSpeed(enemyScriptableObjects[indexEnnemi].movementSpeed);
        newEnemy.GetComponent<SpriteRenderer>().sprite = enemyScriptableObjects[indexEnnemi].enemySprite;
    }
}
