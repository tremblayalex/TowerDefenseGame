using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EnemyScriptableObject[] enemyScriptableObjects;
    public GameObject enemyPrefab;

    public float spawnDelay = 1f;

    private float delayBeforeNextSpawn = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (delayBeforeNextSpawn <= 0)
        {
            SpawnNewEnemy();

            delayBeforeNextSpawn = spawnDelay;
        }

        delayBeforeNextSpawn -= Time.deltaTime;
    }

    private void SpawnNewEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
        newEnemy.GetComponent<Enemy>().setHitPoints(enemyScriptableObjects[0].hitPoints);
        newEnemy.GetComponent<Enemy>().setMovementSpeed(enemyScriptableObjects[0].movementSpeed);
        newEnemy.GetComponent<SpriteRenderer>().sprite = enemyScriptableObjects[0].enemySprite;
    }
}
