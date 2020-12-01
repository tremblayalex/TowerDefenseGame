using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunTower : RotationalTower
{
    protected override void ShootBulletTowardsTarget(GameObject target)
    {
        //GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //newBullet.GetComponent<Bullet>().setTargetEnemy(target);
    }
}
