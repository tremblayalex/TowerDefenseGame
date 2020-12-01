using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTower : Tower
{
    void Awake()
    {
        base.TowerAwake();

        DisplayFireRange();
    }
}
