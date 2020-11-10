using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTower : MonoBehaviour
{
    public TowerScriptableObject[] towerScriptableObjects;
    public GameObject towerPrefab;

    void Start()
    {
        //SpawnNewTower(0, 0);
    }

    public GameObject SpawnNewTower(float x, float y)
    {
        return SpawnNewTower(new Vector3(x, y, 0));
    }

    public GameObject SpawnGhostTower(float x, float y)
    {
        return SpawnGhostTower(new Vector3(x, y, 0));
    }

    public GameObject SpawnNewTower(Vector3 position)//  numeros
    {
        GameObject newTower = Instantiate(towerPrefab, position, Quaternion.identity);
        newTower.GetComponent<Tower>().setDamage(towerScriptableObjects[0].damage);
        newTower.GetComponent<Tower>().setRange(towerScriptableObjects[0].range);
        newTower.GetComponent<Tower>().setFireRate(towerScriptableObjects[0].fireRate);
        newTower.GetComponent<Tower>().setPrice(towerScriptableObjects[0].price);

        newTower.GetComponent<SpriteRenderer>().sprite = towerScriptableObjects[0].towerSprite;

        return newTower;
    }
    public GameObject SpawnGhostTower(Vector3 position)//  numeros
    {
        GameObject newTower = Instantiate(towerPrefab, position, Quaternion.identity);
        newTower.GetComponent<Tower>().setDamage(0);
        newTower.GetComponent<Tower>().setRange(towerScriptableObjects[0].range);
        newTower.GetComponent<Tower>().setFireRate(float.MaxValue);
        newTower.GetComponent<Tower>().DesactivateTower();
        newTower.GetComponent<Tower>().setPrice(0);
        newTower.GetComponent<SpriteRenderer>().sprite = towerScriptableObjects[0].towerSprite;
        newTower.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);


        newTower.name = "ShadowTurret";
        newTower.GetComponent<Tower>().DisplayFireRange();

        return newTower;
    }
}
