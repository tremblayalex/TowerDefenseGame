using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    private float range;
    private bool hidden;

    private LineRenderer lineRenderer;
    private int lineRendererSegments;

    public void setRange(float inRange)
    {
        range = inRange;

        if (lineRenderer != null)
        {
            if (!hidden)
            {
                SetFireRangeSize(range);
            }        
        }
    }

    public float getRange()
    {
        return range;
    }

    void Awake()
    {
        InitializeFireRange();
        hidden = true;
    }

    private void InitializeFireRange()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();    
        
        lineRenderer.useWorldSpace = false;
        lineRenderer.widthMultiplier = 0.05f;
    }

    private void SetFireRangeSize(float rangeRadius)
    {
        lineRendererSegments = (int)range * 10;
        lineRenderer.positionCount = lineRendererSegments + (lineRendererSegments / 25);

        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        for (int i = 0; i < (lineRendererSegments + (lineRendererSegments / 25)); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * rangeRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * rangeRadius;

            lineRenderer.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / lineRendererSegments);
        }
    }

    public void DisplayFireRange()
    {
        hidden = false;
        SetFireRangeSize(range);
    }

    public void HideFireRange()
    {
        hidden = true;
        lineRenderer.positionCount = 0;
    }
}
