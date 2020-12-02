using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTower : Tower
{
    private void Awake()
    {
        base.AwakeTower();
    }

    public void Initialize(Sprite inSprite, float inRange)
    {
        base.InitializeTower(inSprite, inRange);
        
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        gameObject.name = "ShadowTurret";

        DisplayFireRange();
    }
}
