using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public GameObject towerRangePrefab;

    private GameObject towerRangeGameObject;
    private TowerRange towerRange;

    protected float range;

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

    protected void TowerAwake()
    {
        InitializeTowerRange();
    }

    void OnDestroy()
    {
        Destroy(towerRangeGameObject);
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
}
