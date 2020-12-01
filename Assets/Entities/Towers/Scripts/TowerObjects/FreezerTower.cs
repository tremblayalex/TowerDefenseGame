using UnityEngine;

public class FreezerTower : ActivatedTower
{
    //public static FreezerTowerScriptableObject[] freezerTowerScriptableObjects;

    private float freezeTime;
    private float slownessMultiplier;

    public void setFreezeTime(float inFreezeTime)
    {
        freezeTime = inFreezeTime;
    }

    public float getFreezeTime()
    {
        return freezeTime;
    }

    public void setSlownessMultiplier(float inSlownessMultiplier)
    {
        slownessMultiplier = inSlownessMultiplier;
    }

    public float getSlownessMultiplier()
    {
        return slownessMultiplier;
    }

    void Update()
    {
        delayBeforeNextFire -= Time.deltaTime;

        if (AreTargetsInRange() && delayBeforeNextFire <= 0)
        {
            ShootFreezingWave();
            delayBeforeNextFire = fireRate;
        }
    }

    private bool AreTargetsInRange()
    {
        bool areTargetsInRange = false;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        int enemyIndex = 0;
        while (!areTargetsInRange && enemyIndex < allEnemies.Length)
        {
            GameObject enemy = allEnemies[enemyIndex];

            float distanceFromTowerToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceFromTowerToEnemy <= range)
            {
                areTargetsInRange = true;
            }

            enemyIndex++;
        }

        return areTargetsInRange;
    }

    private void ShootFreezingWave()
    {
        // À faire amuse toe
    }
}
