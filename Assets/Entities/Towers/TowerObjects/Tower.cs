using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected TowerSettings towerSettings;

    private GameObject towerRangeGameObject;
    private TowerRange towerRange;

    protected float range;

    protected void InitializeTower(Sprite inSprite, float inRange)
    {
        setSprite(inSprite);

        InitializeTowerRange();
        setRange(inRange);  
    }

    protected void AwakeTower()
    {
        towerSettings = GameObject.Find("TowerSettings").GetComponent<TowerSettings>();
    }

    public void setSprite(Sprite inSprite)
    {
        GetComponent<SpriteRenderer>().sprite = inSprite;
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

    void OnDestroy()
    {
        Destroy(towerRangeGameObject);
    }

    private void InitializeTowerRange()
    {
        towerRangeGameObject = Instantiate(towerSettings.towerRangePrefab, transform.position, Quaternion.identity);
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
}
