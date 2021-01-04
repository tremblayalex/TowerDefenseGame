using System;
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
            MachineGunTowerScriptableObject so = towerSettings.machineGunTowerScriptableObjects[upgradeIndex+1];

            MoneyManager moneyManager = FindObjectOfType<MoneyManager>();
            if (moneyManager.SpendMoney(so.price))
            {
                upgradeIndex++;
                Initialize(so.towerSprite, so.range, so.fireRate, so.price, so.damage);
            }     
        }
        ShowInformationOnSelection();
    }

    public override int MoneyOnSelling()
    {
        int MoneyOnSelling = 0;
        for (int i = upgradeIndex; i >= 0; i--)
        {
            MoneyOnSelling += towerSettings.machineGunTowerScriptableObjects[i].price;
        }
        return Convert.ToInt32(MoneyOnSelling * MoneyPorcentageOnSell);
    }

    protected override void ShootBulletTowardsTarget(GameObject target)
    {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Bullet newBullet = newProjectile.GetComponent<Bullet>();

        newBullet.setDamage(damage);
        newBullet.setTargetEnemy(target);  
    }
}
