using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EnemyScriptableObject[] enemyScriptableObjects;
    public GameObject enemyPrefab;
    public float waveDuration = 60f;
    public float delayBetweenWaves = 15f;
    public int firstWaveEnemyCount = 3;
    public float waveEnemyIncrementMultiplier = 2f;


    private float delayBeforeNextSpawn = 0f;
    private float timeBetweenWaves;
    private int ennemiCount;
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

            float spawnDelay = waveDuration / ennemiCount;
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

        for (int i = 0; i < nombreEnnemie; i++)     
       
       // while (nombreEnnemie > 0)
        {
            //Debug.Log("spawnOneEnemi");
            CustomWave(ref nombreEnnemie);

            yield return new WaitForSeconds(timeBetwenEnnemi);

        }
        waveNumber++;

        print("wave is finish");

    }

    private void CustomWave(ref int nombreEnnemi)
    {
       // Debug.Log("inCustomeWave");
        SpwanFirstEnemyType(ref nombreEnnemi);
        SpwanSecondEnemyType(ref nombreEnnemi);
        SpwanThirdEnemyType(ref nombreEnnemi);
        SpwanFourthEnemyType(ref nombreEnnemi);
        SpwanFifthEnemyType(ref nombreEnnemi);
        SpwanSithEnemyType(ref nombreEnnemi);
        SpwanSeventhEnemyType(ref nombreEnnemi);
        SpwanEightEnemyType(ref nombreEnnemi);
        SpwanNithEnemyType(ref nombreEnnemi);
        SpwanTenthEnemyType(ref nombreEnnemi);
        SpwanEleventhEnemyType(ref nombreEnnemi);
        SpwanTwelvethEnemyType(ref nombreEnnemi);
    }
    #region SpawnEnemytype
    void SpwanFirstEnemyType(ref int nombreEnnemiType) 
    {
        if (nombreEnnemiType <= 10 && nombreEnnemiType > 0)
        {
            SpawnNewEnemy(0);
            nombreEnnemiType--;
        }         
        
    }
    void SpwanSecondEnemyType(ref int nombreEnnemiType)
    {
        if (nombreEnnemiType <= 20 && nombreEnnemiType > 10)
        {
            SpawnNewEnemy(1);
            nombreEnnemiType--;
        }
   
    }
    void SpwanThirdEnemyType(ref int nombreEnnemiType)
    {
        if (nombreEnnemiType<=30 && nombreEnnemiType > 20)
        {
            SpawnNewEnemy(2);
            nombreEnnemiType--;
        }
        

    }
    void SpwanFourthEnemyType(ref int nombreEnnemiType)
    {
        if (nombreEnnemiType <= 40 && nombreEnnemiType > 30)
        {
            SpawnNewEnemy(3);
            nombreEnnemiType--;
        }

    }
    void SpwanFifthEnemyType(ref int nombreEnnemiType)
    {
        if (nombreEnnemiType <= 50 && nombreEnnemiType > 40)
        {
            SpawnNewEnemy(4);
            nombreEnnemiType--;
        }
    }
    void SpwanSithEnemyType(ref int nombreEnnemiType)
    {
        if (nombreEnnemiType <= 60 && nombreEnnemiType > 50)
        {
            SpawnNewEnemy(5);
            nombreEnnemiType--;
        }
    }
    void SpwanSeventhEnemyType(ref int nombreEnnemiType)
    {
        if (nombreEnnemiType <= 70 && nombreEnnemiType > 60)
        {
            SpawnNewEnemy(6);
            nombreEnnemiType--;
        }
    }
    void SpwanEightEnemyType(ref int nombreEnnemiType)
    {
        if (nombreEnnemiType <= 80 && nombreEnnemiType > 70)
        {
            SpawnNewEnemy(7);
            nombreEnnemiType--;
        }
    }
    void SpwanNithEnemyType(ref int nombreEnnemiType)
    {
        if (nombreEnnemiType <= 90 && nombreEnnemiType > 80)
        {
            SpawnNewEnemy(8);
            nombreEnnemiType--;
        }
    }
    void SpwanTenthEnemyType(ref int nombreEnnemiType)
    {
        if (nombreEnnemiType <= 100 && nombreEnnemiType > 90)
        {
            SpawnNewEnemy(9);
            nombreEnnemiType--;
        }
    }
    void SpwanEleventhEnemyType(ref int nombreEnnemiType)
    {
        if (nombreEnnemiType <= 110 && nombreEnnemiType > 100)
        {
            SpawnNewEnemy(10);
            nombreEnnemiType--;
        }
    }
    void SpwanTwelvethEnemyType(ref int nombreEnnemiType)
    {
        if (nombreEnnemiType <= 120 && nombreEnnemiType > 110)
        {
            SpawnNewEnemy(11);
            nombreEnnemiType--;
        }
    }
    #endregion
    private void SpawnNewEnemy(int indexEnnemi)
    {
       
        GameObject newEnemy = Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
        newEnemy.GetComponent<Enemy>().setHitPoints(enemyScriptableObjects[indexEnnemi].hitPoints);

        newEnemy.GetComponent<Enemy>().setMovementSpeed(enemyScriptableObjects[indexEnnemi].movementSpeed);
        newEnemy.GetComponent<SpriteRenderer>().sprite = enemyScriptableObjects[indexEnnemi].enemySprite;
    }
}
