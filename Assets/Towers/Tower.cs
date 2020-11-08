using JetBrains.Annotations;
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

    void Awake()
    {
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

    private float delayTemp = 0;

    void Update()
    {
        GameObject target = FindTargetEnemy();

        delayTemp -= Time.deltaTime; //--------------------------------------<<<

        if (target != null)
        {
            RotateTowardsTarget(target.transform.position);

            if (delayTemp <= 0) //--------------------------------------<<<
            {
                ShootBulletTowardsTarget(target);
                delayTemp = 0.5f; //--------------------------------------<<<
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
