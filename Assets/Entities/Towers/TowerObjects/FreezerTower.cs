using System.Linq;
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

    public override void ShowInformationOnSelection()
    {
        if (upgradeIndex + 1 < towerSettings.freezerTowerScriptableObjects.Length)
        {
            FreezerTowerScriptableObject so = towerSettings.freezerTowerScriptableObjects[upgradeIndex+1];
            uiManager.DisplayInformationsTowerSelected(so.price, MoneyOnSelling());
        }
        else
        {
            uiManager.DisplayInformationsTowerSelected(MoneyOnSelling());
        }
    }

    public override void Upgrade()
    {
        print("-- Attempt Upgrade Freeze Tower---");
        if (upgradeIndex + 1 < towerSettings.freezerTowerScriptableObjects.Length)
        {
            print("Upgrade Available");

            FreezerTowerScriptableObject so = towerSettings.freezerTowerScriptableObjects[upgradeIndex+1];
            MoneyManager moneyManager = FindObjectOfType<MoneyManager>();
            if (moneyManager.SpendMoney(so.price))
            {
                print("Price money : " + so.price);
                print("Present Upgrade Index : " +upgradeIndex);
                Initialize(so.towerSprite, so.range, so.fireRate, so.price, so.freezeTime, so.slownessMultiplier);
                upgradeIndex++;
            }
        }
        else
        {
            print("No more Upgrade");
        }
        ShowInformationOnSelection();
    }
    public override int MoneyOnSelling()
    {
        int MoneyOnSelling = 0;
        for (int i = upgradeIndex; i >= 0; i--)
        {
            MoneyOnSelling += towerSettings.freezerTowerScriptableObjects[i].price;
        }
        return MoneyOnSelling / 2;
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
