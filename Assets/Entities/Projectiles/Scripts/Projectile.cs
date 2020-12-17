using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float speed;
    public AudioSoundEffect fireSound;

    protected SoundPlayer soundPlayer;

    protected int damage;
    protected GameObject targetEnemy; 

    public void setDamage(int inDamage)
    {
        damage = inDamage;
    }

    public void setTargetEnemy(GameObject inTargetEnemy)
    {
        targetEnemy = inTargetEnemy;
    }

    private void Start()
    {
        soundPlayer = GameObject.FindGameObjectWithTag("SoundPlayer").GetComponent<SoundPlayer>();
        PlayFireSound();
    }

    private void PlayFireSound()
    {
        soundPlayer = FindObjectOfType<SoundPlayer>();
        soundPlayer.PlaySound(fireSound);
    }

    void Update()
    {
        if (targetEnemy != null)
        {
            MoveTowardsEnemy();

            if (transform.position == targetEnemy.transform.position)
            {
                ProjectileReachedTarget(); 
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected abstract void MoveTowardsEnemy();

    protected abstract void ProjectileReachedTarget();

    protected void TranslateTowardsEnemy()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, 6 * Time.deltaTime);
        transform.position = newPosition;
    }
}
