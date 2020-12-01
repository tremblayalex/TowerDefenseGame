using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActivatedTower : Tower
{
    protected float fireRate;
    protected float price;

    protected float delayBeforeNextFire = 0f;

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
