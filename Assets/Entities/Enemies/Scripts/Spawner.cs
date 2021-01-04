using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EnemyScriptableObject[] enemyScriptableObjects;
    public int[] numberOfEnnemyNeededToSpawnThisEnnemi;
    public GameObject enemyPrefab;
    public UIManager uiManager;
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
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
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
            delayBeforeNextSpawn = timeBetweenWaves;
        }

        delayBeforeNextSpawn -= Time.deltaTime;
    }

    IEnumerator SpawnAWave(int numberEnnemie, float timeBetwenEnnemi)
    {  
        while (numberEnnemie > 0)
        {
            //Debug.Log("spawnOneEnemi");
            CustomWave(ref numberEnnemie);

            yield return new WaitForSeconds(timeBetwenEnnemi);

        }
        waveNumber++;
    }

    private void CustomWave(ref int numberEnnemi)
    {
        for (int i = 0; i < enemyScriptableObjects.Length-1; i++)
        {
            SpwanEnemyType(ref numberEnnemi, i, numberOfEnnemyNeededToSpawnThisEnnemi[i+1], numberOfEnnemyNeededToSpawnThisEnnemi[i]);
        }
        SpwanEnemyType(ref numberEnnemi, enemyScriptableObjects.Length-1, numberOfEnnemyNeededToSpawnThisEnnemi[enemyScriptableObjects.Length-1]);

    }
    #region SpawnEnemytype
    void SpwanEnemyType(ref int numberEnnemiType, int ennemiType, int max, int min)
    {
        if (numberEnnemiType <= max && numberEnnemiType > min)
        {
            SpawnNewEnemy(ennemiType);
            numberEnnemiType--;
        }
    }

    void SpwanEnemyType(ref int numberEnnemiType, int ennemiType, int min)
    {
        if (numberEnnemiType > min)
        {
            SpawnNewEnemy(ennemiType);
            numberEnnemiType--;
        }
    }
    /*
     * How ma partener programmed the spawn of the different ennemies.
    
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

    void SpwanFirstEnemyType(ref int numberEnnemiType)
    {
        if (numberEnnemiType <= 10 && numberEnnemiType > 0)
        {
            SpawnNewEnemy(0);
            numberEnnemiType--;
        }

    }
    void SpwanSecondEnemyType(ref int numberEnnemiType)
    {
        if (numberEnnemiType <= 20 && numberEnnemiType > 10)
        {
            SpawnNewEnemy(1);
            numberEnnemiType--;
        }

    }
    void SpwanThirdEnemyType(ref int numberEnnemiType)
    {
        if (numberEnnemiType <= 30 && numberEnnemiType > 20)
        {
            SpawnNewEnemy(2);
            numberEnnemiType--;
        }


    }
    void SpwanFourthEnemyType(ref int numberEnnemiType)
    {
        if (numberEnnemiType <= 40 && numberEnnemiType > 30)
        {
            SpawnNewEnemy(3);
            numberEnnemiType--;
        }

    }
    void SpwanFifthEnemyType(ref int numberEnnemiType)
    {
        if (numberEnnemiType <= 50 && numberEnnemiType > 40)
        {
            SpawnNewEnemy(4);
            numberEnnemiType--;
        }
    }
    void SpwanSithEnemyType(ref int numberEnnemiType)
    {
        if (numberEnnemiType <= 60 && numberEnnemiType > 50)
        {
            SpawnNewEnemy(5);
            numberEnnemiType--;
        }
    }
    void SpwanSeventhEnemyType(ref int numberEnnemiType)
    {
        if (numberEnnemiType <= 70 && numberEnnemiType > 60)
        {
            SpawnNewEnemy(6);
            numberEnnemiType--;
        }
    }
    void SpwanEightEnemyType(ref int numberEnnemiType)
    {
        if (numberEnnemiType <= 80 && numberEnnemiType > 70)
        {
            SpawnNewEnemy(7);
            numberEnnemiType--;
        }
    }
    void SpwanNithEnemyType(ref int numberEnnemiType)
    {
        if (numberEnnemiType <= 90 && numberEnnemiType > 80)
        {
            SpawnNewEnemy(8);
            numberEnnemiType--;
        }
    }
    void SpwanTenthEnemyType(ref int numberEnnemiType)
    {
        if (numberEnnemiType <= 100 && numberEnnemiType > 90)
        {
            SpawnNewEnemy(9);
            numberEnnemiType--;
        }
    }
    void SpwanEleventhEnemyType(ref int numberEnnemiType)
    {
        if (numberEnnemiType <= 110 && numberEnnemiType > 100)
        {
            SpawnNewEnemy(10);
            numberEnnemiType--;
        }
    }
    void SpwanTwelvethEnemyType(ref int numberEnnemiType)
    {
        if (numberEnnemiType > 110)
        {
            SpawnNewEnemy(11);
            numberEnnemiType--;
        }
    }
    */
    #endregion
    private void SpawnNewEnemy(int indexEnnemi)
    {

        GameObject newEnemy = Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
        newEnemy.GetComponent<Enemy>().setHitPoints(enemyScriptableObjects[indexEnnemi].hitPoints);

        newEnemy.GetComponent<Enemy>().setMovementSpeed(enemyScriptableObjects[indexEnnemi].movementSpeed);
        newEnemy.GetComponent<Enemy>().setDropMoney(enemyScriptableObjects[indexEnnemi].dropMoney);
        newEnemy.GetComponent<Enemy>().setDammageEndOfPath(enemyScriptableObjects[indexEnnemi].dammageEndOfPath);
        newEnemy.GetComponent<SpriteRenderer>().sprite = enemyScriptableObjects[indexEnnemi].enemySprite;
    }
}
