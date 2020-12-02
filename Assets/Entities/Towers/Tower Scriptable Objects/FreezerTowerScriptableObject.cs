using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezerTowerScriptableObject", menuName = "ScriptableObjects/FreezerTower", order = 1)]
public class FreezerTowerScriptableObject : TowerScriptableObject
{
    public float freezeTime;
    
    [Range(0, 1)]
    public float slownessMultiplier;
}
