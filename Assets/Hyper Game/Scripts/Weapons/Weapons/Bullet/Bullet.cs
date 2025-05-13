using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletBase
{
    void Start()
    {
        BulletSoundManager soundManager = GetComponent<BulletSoundManager>();
        BulletMovement movement = GetComponent<BulletMovement>();
        Initialize(soundManager, movement);
    }
    protected override void HandleCollision(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                HandleHitEffect(other);
                soundManager?.PlayHitSound();
                enemy.TakeDamage(damage);
                ApplyAreaDamage(other.transform.position, 1.0f,other.gameObject); 
            }
        }
    }

    protected void ApplyAreaDamage(Vector2 explosionCenter, float explosionRadius, GameObject mainTarget)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(explosionCenter, explosionRadius);

        foreach (Collider2D hit in hitEnemies)
        {
            if (hit.CompareTag("Enemy") && hit.gameObject != mainTarget) // Chỉ gây sát thương cho Enemy
            {
                EnemyBase enemy = hit.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage / 2); // Sát thương lan là 50% sát thương chính
                }
            }
        }
    }
}
