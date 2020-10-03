using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EnemyScriptableObject[] enemyScriptableObjects;
    public GameObject enemyPrefab;

    private GameObject enemy;

    void Start()
    {
        CreateNewTestEnemy();
    }

    private void CreateNewTestEnemy()
    {
        enemy = Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
        enemy.GetComponent<Enemy>().HitPoints = enemyScriptableObjects[0].hitPoints;
        enemy.GetComponent<Enemy>().MovementSpeed = enemyScriptableObjects[0].movementSpeed;
        enemy.GetComponent<SpriteRenderer>().sprite = enemyScriptableObjects[0].enemySprite;
    }

    void Update()
    {
        
    }
}
