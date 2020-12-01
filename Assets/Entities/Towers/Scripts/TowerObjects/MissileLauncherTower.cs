using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLaucherTower : RotationalTower
{
    private int explosionRange;

    public void setExplosionRange(int inExplosionRange)
    {
        explosionRange = inExplosionRange;
    }

    public int getExplosionRange()
    {
        return explosionRange;
    }

    protected override void ShootBulletTowardsTarget(GameObject target)
    {
        //GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //newBullet.GetComponent<Bullet>().setTargetEnemy(target);
    }
}
