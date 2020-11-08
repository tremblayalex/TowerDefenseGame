using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{    
    public float speed = 25f;
    public float explosionRange = 1f;
    public int explosionDamage = 5;
  
    public AudioSoundEffect bulletFireEffect;
    public AudioSoundEffect bulletExplosionEffect;

    private SoundPlayer soundPlayer;
    private GameObject targetEnemy;

    public void setTargetEnemy(GameObject inTargetEnemy)
    {
        targetEnemy = inTargetEnemy;
    }

    void Start()
    {
        soundPlayer = GameObject.FindGameObjectWithTag("SoundPlayer").GetComponent<SoundPlayer>();
        PlayFireSound();
    }

    private void PlayFireSound()
    {
        soundPlayer.PlaySound(bulletFireEffect);
    }

    void Update()
    {
        if (targetEnemy != null)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, speed * Time.deltaTime);
            transform.position = newPosition;

            if (transform.position == targetEnemy.transform.position)
            {
                Explode();
            }
        }
        else
        {
            Explode();
        }
    }

    private void Explode()
    {
        DamageAllNearbyEnemies();
        SpawnExplosionParticleEffect();
        PlayExplosionSound();

        Destroy(gameObject);
    }

    private void DamageAllNearbyEnemies()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in allEnemies)
        {
            float distanceFromBulletToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceFromBulletToEnemy <= explosionRange)
            {
                enemy.GetComponent<Enemy>().Damage(explosionDamage);
            }
        }
    }

    private void SpawnExplosionParticleEffect()
    {
        // -------------------------------------------------------------------<<<<<<
    }

    private void PlayExplosionSound()
    {
        soundPlayer.PlaySound(bulletExplosionEffect);
    }
}
