using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
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
    }

    public float getRange()
    {
        return range;
    }

    void Start()
    {
        
    }

    void Update()
    {
        // Find the next enemy target
        // The target must be within range
        // The target must be the one that has made the most progress in the parkour
        // (The one closest to completing the parkour)
        FindTargetEnemy();

        // Rotate Towards Enemy

        // Shoot Bullet Towards Enemy



        // --- To code in the Bullet Script ---
        // A shooting sounds plays when it is spawned
        //
        // The bullet must move towards the enemy (to ajust for enemy movement)
        //
        // Two options for contact with enemy:
        //      > The bullet check distance with enemy
        //        Easy to do but requieres checking manually
        //      > The bullet will use IsTrigger
        //        Uses Unity
        //
        // To make it more interesting:
        // The bullet could damage all nearby enemies instead of only damaging the enemy it shot at
        // More logical since the bullet will explode
        //
        // Explosion effects:
        //      > Explosion particles are displayed
        //      > An explosion sound is played



        // --- To code in the Enemy Script ---
        // Once the enemy has no more hitPoints, it dies
        //
        // When an enemy dies, money is rewarded
        // (Add a money property to the scriptable object? More difficult enemies should give more money)
    }

    private GameObject FindTargetEnemy()
    {
        GameObject nearestEnemy = null;
        float distanceToNearestEnemy = range;

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in allEnemies)
        {
            float distanceFromTower = Vector3.Distance(gameObject.transform.position, enemy.transform.position);

            if (distanceFromTower <= range)
            {
                float x = enemy.transform.position.x;
                float y = enemy.transform.position.y;

                print("X:" + x + ", Y:" + y + " -- Distance:" + distanceFromTower);
            }
        }
        
        return nearestEnemy;
    }
}
