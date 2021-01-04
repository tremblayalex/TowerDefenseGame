using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTower : MonoBehaviour
{
    private TowerSettings towerSettings;

    void Start()
    {
        towerSettings = GameObject.Find("TowerSettings").GetComponent<TowerSettings>();
    }

    public GameObject SpawnShadowTower(Vector3 position, int towerIndex)
    {
        GameObject newShadowTower = null;

        switch (towerIndex)
        {
            case 0:
                newShadowTower = SpawnShadowTowerMachineGun(position);
                break;

            case 1:
                newShadowTower = SpawnShadowTowerMissileLauncher(position);
                break;

            case 2:
                newShadowTower = SpawnShadowTowerFreezer(position);
                break;
        }

        return newShadowTower;
    }

    public GameObject SpawnShadowTowerMachineGun(Vector3 position)
    {
        Sprite sprite = towerSettings.machineGunTowerScriptableObjects[0].towerSprite;
        float range = towerSettings.machineGunTowerScriptableObjects[0].range;
        
        return SpawnShadowTower(position, sprite, range);
    }

    public GameObject SpawnShadowTowerMissileLauncher(Vector3 position)
    {
        Sprite sprite = towerSettings.missileLauncherTowerScriptableObjects[0].towerSprite;
        float range = towerSettings.missileLauncherTowerScriptableObjects[0].range;

        return SpawnShadowTower(position, sprite, range);
    }

    public GameObject SpawnShadowTowerFreezer(Vector3 position)
    {
        Sprite sprite = towerSettings.freezerTowerScriptableObjects[0].towerSprite;
        float range = towerSettings.freezerTowerScriptableObjects[0].range;

        return SpawnShadowTower(position, sprite, range);
    }

    private GameObject SpawnShadowTower(Vector3 position, Sprite sprite, float range)
    {
        GameObject shadowTower = Instantiate(towerSettings.shadowTowerPrefab, position, Quaternion.identity);
        shadowTower.GetComponent<ShadowTower>().Initialize(sprite, range);
        return shadowTower;
    }

    public GameObject SpawnTower(Vector3 position, int towerIndex)
    {
        GameObject newTower = null;

        switch (towerIndex)
        {
            case 0:
                newTower = SpawnMachineGunTower(position);
                break;

            case 1:
                newTower = SpawnMissileLauncherTower(position);
                break;

            case 2:
                newTower = SpawnFreezerTower(position);
                break;
        }

        return newTower;
    }

    public GameObject SpawnMachineGunTower(Vector3 position)
    {
        return Instantiate(towerSettings.machineGunTowerPrefab, position, Quaternion.identity);
    }

    public GameObject SpawnMissileLauncherTower(Vector3 position)
    {
        return Instantiate(towerSettings.missileLauncherTowerPrefab, position, Quaternion.identity);
    }

    public GameObject SpawnFreezerTower(Vector3 position)
    {
        return Instantiate(towerSettings.freezerTowerPrefab, position, Quaternion.identity);
    }
}
