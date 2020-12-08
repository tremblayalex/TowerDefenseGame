using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float animationSpeed;
    public float animationFadeDuration;

    private float explosionRange;

    private float animationRadius;
    private float animationAlpha;
    private float animationAlphaStep;
    private SpriteRenderer spriteRenderer;

    public void setExplosionRange(float inExplosionRange)
    {
        explosionRange = inExplosionRange;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animationRadius = 0;
        animationAlpha = spriteRenderer.color.a;
        animationAlphaStep = animationAlpha / animationFadeDuration;
    }

    void Update()
    {
        if (animationRadius < explosionRange)
        {
            animationRadius += animationSpeed * Time.deltaTime;

            ChangeCircleRadius(animationRadius);
        }
        else
        {
            animationRadius = explosionRange;
            ChangeCircleRadius(animationRadius);

            animationAlpha -= animationAlphaStep * Time.deltaTime;

            if (animationAlpha > 0)
            {
                ChangeCircleAlpha(animationAlpha);
            }
            else
            {
                animationAlpha = 0;
                ChangeCircleAlpha(animationAlpha);
                Destroy(gameObject);
            }
        }
    }

    private void ChangeCircleRadius(float circleRadius)
    {
        float circleDiameter = circleRadius * 2;
        transform.localScale = new Vector3(circleDiameter, circleDiameter, transform.localScale.z);
    }

    private void ChangeCircleAlpha(float circleAlpha)
    {
        Color oldColor = spriteRenderer.color;
        spriteRenderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, circleAlpha);
    }
}
