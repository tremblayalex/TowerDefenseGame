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
        Destroy(gameObject);
    }

    private void DamageTarget()
    {
        if (targetEnemy == null)
        {
            Destroy(gameObject);
        }
        else
        {
            targetEnemy.GetComponent<Enemy>().Damage(damage);
        }       
    }
}
