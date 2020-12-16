﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MachineGunTower : RotationalTower
{


    protected void Initialize(Sprite inSprite, float inRange, float inFireRate, float inPrice, int inDamage)
    {
        base.InitializeRotationalTower(inSprite, inRange, inFireRate, inPrice, inDamage);
    }

    protected void Awake()
    {
        base.AwakeRotationalTower();
        

        MachineGunTowerScriptableObject so = towerSettings.machineGunTowerScriptableObjects[0];
        Initialize(so.towerSprite, so.range, so.fireRate, so.price, so.damage);       
    }

    public override void ShowInformationOnSelection()
    {
        if (upgradeIndex + 1 < towerSettings.machineGunTowerScriptableObjects.Length)
        {
            MachineGunTowerScriptableObject so = towerSettings.machineGunTowerScriptableObjects[upgradeIndex+1];
            uiManager.DisplayInformationsTowerSelected(so.price, MoneyOnSelling());
        }
        else
        {
            uiManager.DisplayInformationsTowerSelected(MoneyOnSelling());
        }
    }

    public override void Upgrade()
    {
        if (upgradeIndex + 1 < towerSettings.machineGunTowerScriptableObjects.Length)
        {

            print("UPGRADE MACHINE");
            

            MachineGunTowerScriptableObject so = towerSettings.machineGunTowerScriptableObjects[upgradeIndex+1];
            Debug.Log("Index: " + upgradeIndex);
            Debug.Log("ScriptObject: " + towerSettings.machineGunTowerScriptableObjects.Length);

            MoneyManager moneyManager = FindObjectOfType<MoneyManager>();
            if (moneyManager.SpendMoney(so.price))
            {
                upgradeIndex++;
                Initialize(so.towerSprite, so.range, so.fireRate, so.price, so.damage);
            }
            
        }
    }

    public override int MoneyOnSelling()
    {
        int MoneyOnSelling = 0;
        for (int i = upgradeIndex; i >= 0; i--)
        {
            MoneyOnSelling += towerSettings.machineGunTowerScriptableObjects[i].price;
        }
        return MoneyOnSelling / 2;
    }

    protected override void ShootBulletTowardsTarget(GameObject target)
    {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Bullet newBullet = newProjectile.GetComponent<Bullet>();

        newBullet.setDamage(damage);
        newBullet.setTargetEnemy(target);  
    }
}