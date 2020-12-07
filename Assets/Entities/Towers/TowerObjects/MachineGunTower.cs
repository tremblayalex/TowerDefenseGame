using System.Collections;
using System.Collections.Generic;
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

    protected override void ShootBulletTowardsTarget(GameObject target)
    {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Bullet newBullet = newProjectile.GetComponent<Bullet>();

        newBullet.setDamage(damage);
        newBullet.setTargetEnemy(target);  
    }
}
