using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    public AudioSoundEffect explosionSound;
    public GameObject explosionEffectPrefab;

    private float explosionRange;

    public void setExplosionRange(float inExplosionRange)
    {
        explosionRange = inExplosionRange * 2;
    }

    protected override void MoveTowardsEnemy()
    {
        TranslateTowardsEnemy();
        RotateTowardsEnemy();
    }

    private void RotateTowardsEnemy()
    {
        float angleSpriteCorrection = -90;

        Vector2 current = transform.position;
        Vector2 targetPosition2D = targetEnemy.transform.position;
        var direction = targetPosition2D - current;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + angleSpriteCorrection, Vector3.forward);
    }

    protected override void ProjectileReachedTarget()
    {
        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        DamageAllNearbyEnemies();
        SpawnExplosionParticleEffect();
        PlayExplosionSound();
    }

    private void DamageAllNearbyEnemies()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in allEnemies)
        {
            float distanceFromBulletToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceFromBulletToEnemy <= explosionRange)
            {
                enemy.GetComponent<Enemy>().Damage(damage);
            }
        }
    }

    private void SpawnExplosionParticleEffect()
    {
        GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        explosionEffect.GetComponentInChildren<ChangeShaderColor>().setExplosionRange(explosionRange);
    }

    private void PlayExplosionSound()
    {
        soundPlayer = FindObjectOfType<SoundPlayer>();
        soundPlayer.PlaySound(explosionSound);
    }
}