using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTower : MonoBehaviour
{
    public TowerScriptableObject[] towerScriptableObjects;
    public GameObject towerPrefab;

    void Start()
    {
        SpawnNewTower(0, 0);
    }

    void Update()
    {
        
    }

    public void SpawnNewTower(float x, float y)
    {
        SpawnNewTower(new Vector3(x, y, 0));
    }

    public void SpawnNewTower(Vector3 position)
    {
        GameObject newTower = Instantiate(towerPrefab, position, Quaternion.identity);
        newTower.GetComponent<Tower>().setDamage(towerScriptableObjects[0].damage);
        newTower.GetComponent<Tower>().setRange(towerScriptableObjects[0].range);
        newTower.GetComponent<SpriteRenderer>().sprite = towerScriptableObjects[0].towerSprite;
    }
}
