using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSettings : MonoBehaviour
{
    public GameObject shadowTowerPrefab;
    public GameObject machineGunTowerPrefab;
    public GameObject missileLauncherTowerPrefab;
    public GameObject freezerTowerPrefab;

    public GameObject towerRangePrefab;

    public MachineGunTowerScriptableObject[] machineGunTowerScriptableObjects;
    public MissileLauncherTowerScriptableObject[] missileLauncherTowerScriptableObjects;
    public FreezerTowerScriptableObject[] freezerTowerScriptableObjects;
}
