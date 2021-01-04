using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActivatedTower : Tower
{
    

    protected float fireRate;
    protected float price;

    protected float delayBeforeNextFire = 0f;
    protected int upgradeIndex;

    protected float MoneyPorcentageOnSell = 0.80f;

    protected void InitializeActivatedTower(Sprite inSprite, float inRange, float inFireRate, float inPrice)
    {
        base.InitializeTower(inSprite, inRange);

        fireRate = inFireRate;
        price = inPrice;
        
    }

    protected void AwakeActivatedTower()
    {
        base.AwakeTower();
        upgradeIndex = 0;
    }

    public abstract void Upgrade();
    public abstract void ShowInformationOnSelection();


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public void Sell()
    {

        MoneyManager moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();        
        moneyManager.AddMoney(MoneyOnSelling());
    }

    public abstract int MoneyOnSelling();
    
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


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
}
