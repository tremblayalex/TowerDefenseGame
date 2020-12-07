using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{    
    protected override void MoveTowardsEnemy()
    {
        TranslateTowardsEnemy();
    }

    protected override void ProjectileReachedTarget()
    {
        DamageTarget();
    }

    private void DamageTarget()
    {
        targetEnemy.GetComponent<Enemy>().Damage(damage);
    }
}
