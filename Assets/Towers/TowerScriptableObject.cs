using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerScriptableObject", menuName = "ScriptableObjects/TowerScriptableObject", order = 1)]
public class TowerScriptableObject : ScriptableObject
{
    public Sprite towerSprite;
    public int damage;
    public float range;
    public float fireRate;
    public float price;
}
