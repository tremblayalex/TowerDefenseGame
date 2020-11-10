using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/EnemyScriptableObject", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public Sprite enemySprite;
    public int hitPoints;
    public float movementSpeed;
    public int dropMoney;
    public int dammageEndOfPath;
}
