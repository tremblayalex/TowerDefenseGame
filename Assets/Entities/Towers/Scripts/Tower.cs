﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject towerRangePrefab;
    public GameObject bulletPrefab;

    private GameObject towerRangeGameObject;
    private TowerRange towerRange;

    private int damage;
    private float range;
    private float fireRate;
    private float price;

    private bool activated;

    private float delayBeforeNextFire = 0f;

    public void setDamage(int inDamage)
    {
        damage = inDamage;
    }

    public int getDamage()
    {
        return damage;
    }

    public void setRange(float inRange)
    {
        range = inRange;

        if (towerRange != null)
        {
            towerRange.setRange(range);
        }
    }

    public float getRange()
    {
        return range;
    }

    public void setFireRate(float inFireRate)
    {
        fireRate = inFireRate;
    }

    public float getFireRate()
    {
        return fireRate;
    }

    public void setPrice(float inPrice)
    {
        price = inPrice;
    }

    public float getPrice(float price)
    {
        return price;
    }

    void Awake()
    {
        activated = true;
        InitializeTowerRange();     
    }

    private void InitializeTowerRange()
    {
        towerRangeGameObject = Instantiate(towerRangePrefab, transform.position, Quaternion.identity);
        towerRange = towerRangeGameObject.GetComponent<TowerRange>();
    }

    public void DisplayFireRange()
    {
        towerRange.DisplayFireRange();
    }

    public void HideFireRange()
    {
        towerRange.HideFireRange();
    }

    public void ActivateTower()
    {
        activated = true;
    }

    public void DesactivateTower()
    {
        activated = false;
    }

    void Update()
    {
        if (activated)
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
    }

    void OnDestroy()
    {
        Destroy(towerRangeGameObject);
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

    private void ShootBulletTowardsTarget(GameObject target)
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().setTargetEnemy(target);
    }
}