using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private int damage;
    private float range;

    public void setDamage(int inDamage)
    {
        damage = inDamage;
    }

    public int getDamage()
    {
        return damage;
    }

    public void setRange(float inRange)
    {
        range = inRange;
    }

    public float getRange()
    {
        return range;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
