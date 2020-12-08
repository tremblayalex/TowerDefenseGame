using UnityEngine;

public class FreezerTower : ActivatedTower
{
    public GameObject freezeWavePrefab;

    private float freezeTime;
    private float slownessMultiplier;

    private void Initialize(Sprite inSprite, float inRange, float inFireRate, float inPrice, float inFreezeTime, float inSlownessMultiplier)
    {
        base.InitializeActivatedTower(inSprite, inRange, inFireRate, inPrice);

        freezeTime = inFreezeTime;
        slownessMultiplier = inSlownessMultiplier;     
    }

    protected void Awake()
    {
        base.AwakeActivatedTower();

        FreezerTowerScriptableObject so = towerSettings.freezerTowerScriptableObjects[0];
        Initialize(so.towerSprite, so.range, so.fireRate, so.price, so.freezeTime, so.slownessMultiplier);        
    }

    public override void Upgrade()
    {
        if (upgradeIndex + 1 < towerSettings.freezerTowerScriptableObjects.Length)
        {
            upgradeIndex++;

            FreezerTowerScriptableObject so = towerSettings.freezerTowerScriptableObjects[upgradeIndex];
            Initialize(so.towerSprite, so.range, so.fireRate, so.price, so.freezeTime, so.slownessMultiplier);
        }
    }

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
        GameObject newFreezeWaveObject = Instantiate(freezeWavePrefab, transform.position, Quaternion.identity);
        FreezeWave newFreezeWave = newFreezeWaveObject.GetComponent<FreezeWave>();

        newFreezeWave.setFreezeRange(range);
        newFreezeWave.setFreezeTime(freezeTime);
        newFreezeWave.setSlownessMultiplier(slownessMultiplier);
    }
}
