using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissileLauncherTowerScriptableObject", menuName = "ScriptableObjects/MissileLauncherTower", order = 1)]
public class MissileLauncherTowerScriptableObject : RotationalTowerScriptableObject
{
    public float explosionRange;
}
