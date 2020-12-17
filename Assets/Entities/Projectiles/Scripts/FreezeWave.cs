using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeWave : MonoBehaviour
{
    protected SoundPlayer soundPlayer;
    public AudioSoundEffect freezeSound;

    public float animationSpeed;
    public float animationFadeDuration;

    private float freezeRange;
    private float freezeTime;
    private float slownessMultiplier;

    private float animationRadius;
    private float animationAlpha;
    private float animationAlphaStep;
    private SpriteRenderer spriteRenderer;

    public void setFreezeRange(float inFreezeRange)
    {
        freezeRange = inFreezeRange;
    }

    public void setFreezeTime(float inFreezeTime)
    {
        freezeTime = inFreezeTime;
    }

    public void setSlownessMultiplier(float inSlownessMultiplier)
    {
        slownessMultiplier = inSlownessMultiplier;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animationRadius = 0;
        animationAlpha = spriteRenderer.color.a;
        animationAlphaStep = animationAlpha / animationFadeDuration;

        FreezeAllNearbyEnemies();
        ChangeCircleRadius(animationRadius);
        PlayFreezeSound();
    }

    
    void Update()
    {
        if (animationRadius < freezeRange)
        {
            animationRadius += animationSpeed * Time.deltaTime;

            ChangeCircleRadius(animationRadius);
        }
        else
        {
            animationRadius = freezeRange;
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

    private void FreezeAllNearbyEnemies()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in allEnemies)
        {
            float distanceFromBulletToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceFromBulletToEnemy <= freezeRange)
            {
                enemy.GetComponent<Enemy>().Freeze(freezeTime, slownessMultiplier);
            }
        }
    }

    private void PlayFreezeSound()
    {
        soundPlayer = FindObjectOfType<SoundPlayer>();
        soundPlayer.PlaySound(freezeSound);
    }
}