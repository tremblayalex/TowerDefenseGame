﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissileLauncherTower : RotationalTower
{
    private float explosionRange;

    protected void Initialize(Sprite inSprite, float inRange, float inFireRate, float inPrice, int inDamage, float inExplosionRange)
    {
        base.InitializeRotationalTower(inSprite, inRange, inFireRate, inPrice, inDamage);

        explosionRange = inExplosionRange;   
    }

    private void Awake()
    {
        base.AwakeRotationalTower();

        MissileLauncherTowerScriptableObject so = towerSettings.missileLauncherTowerScriptableObjects[0];
        Initialize(so.towerSprite, so.range, so.fireRate, so.price, so.damage, so.explosionRange);        
    }

    public override void ShowInformationOnSelection()
    {
        if (upgradeIndex + 1 < towerSettings.missileLauncherTowerScriptableObjects.Length)
        {
            MissileLauncherTowerScriptableObject so = towerSettings.missileLauncherTowerScriptableObjects[upgradeIndex + 1];
            uiManager.DisplayInformationsTowerSelected(so.price, MoneyOnSelling());
        }
        else
        {
            uiManager.DisplayInformationsTowerSelected(MoneyOnSelling());
        }
    }

    public override void Upgrade()
    {        
        if (upgradeIndex + 1 < towerSettings.missileLauncherTowerScriptableObjects.Length)
        {
            MissileLauncherTowerScriptableObject so = towerSettings.missileLauncherTowerScriptableObjects[upgradeIndex+1];
            MoneyManager moneyManager = FindObjectOfType<MoneyManager>();
            if (moneyManager.SpendMoney(so.price))
            {
                Initialize(so.towerSprite, so.range, so.fireRate, so.price, so.damage, so.explosionRange);
                upgradeIndex++;
            }
        }
        ShowInformationOnSelection();
    }

    public override int MoneyOnSelling()
    {
        int MoneyOnSelling = 0;
        for (int i = upgradeIndex; i >= 0; i--)
        {
            MoneyOnSelling += towerSettings.missileLauncherTowerScriptableObjects[i].price;
        }
        return Convert.ToInt32(MoneyOnSelling * MoneyPorcentageOnSell);
    }


    public void setExplosionRange(float inExplosionRange)
    {
        explosionRange = inExplosionRange;
    }

    public float getExplosionRange()
    {
        return explosionRange;
    }

    protected override void ShootBulletTowardsTarget(GameObject target)
    {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Missile newMissile = newProjectile.GetComponent<Missile>();

        newMissile.setDamage(damage);
        newMissile.setExplosionRange(explosionRange);
        newMissile.setTargetEnemy(target);
    }
}
