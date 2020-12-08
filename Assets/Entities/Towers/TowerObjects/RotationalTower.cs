using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RotationalTower : ActivatedTower
{
    public GameObject projectilePrefab;

    protected int damage;

    protected void InitializeRotationalTower(Sprite inSprite, float inRange, float inFireRate, float inPrice, int inDamage)
    {
        base.InitializeActivatedTower(inSprite, inRange, inFireRate, inPrice);

        damage = inDamage; 
    }

    protected void AwakeRotationalTower()
    {
        base.AwakeActivatedTower();
    }

    public override abstract void Upgrade();

    public void setDamage(int inDamage)
    {
        damage = inDamage;
    }

    public int getDamage()
    {
        return damage;
    }

    void Update()
    {
        GameObject target = FindTargetEnemy();

        delayBeforeNextFire -= Time.deltaTime;

        if (target != null)
        {
            RotateTowardsTarget(target.transform.position);

            if (delayBeforeNextFire <= 0)
            {
                ShootBulletTowardsTarget(target);
                delayBeforeNextFire = fireRate;
            }
        }
    }

    private GameObject FindTargetEnemy()
    {
        GameObject targetEnemy = null;
        float maxDistanceTravelled = 0;

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in allEnemies)
        {
            float distanceFromTowerToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceFromTowerToEnemy <= range)
            {
                float distanceTravelled = enemy.GetComponent<Enemy>().getDistanceTravelled();

                if (distanceTravelled > maxDistanceTravelled)
                {
                    targetEnemy = enemy;

                    maxDistanceTravelled = distanceTravelled;
                }
            }
        }

        return targetEnemy;
    }

    private void RotateTowardsTarget(Vector3 targetPosition)
    {
        float angleSpriteCorrection = -90;

        Vector2 current = transform.position;
        Vector2 targetPosition2D = targetPosition;
        var direction = targetPosition2D - current;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + angleSpriteCorrection, Vector3.forward);
    }

    protected abstract void ShootBulletTowardsTarget(GameObject target);
}