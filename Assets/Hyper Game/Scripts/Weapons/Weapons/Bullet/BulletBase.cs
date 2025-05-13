using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed = 10f;
    [SerializeField] protected int damage = 1;

    protected BulletSoundManager soundManager;
    private BulletMovement movement;
    [SerializeField] private GameObject hitEffect;
    

    // void Start()
    // {
    //     HandleAura();
    // }
    public void Initialize(BulletSoundManager soundManager, BulletMovement movement)
    {
        this.soundManager = soundManager;
        this.movement = movement;

        if (soundManager != null)
        {
            soundManager.PlayShootSound();
        }
        movement.Initialize(bulletSpeed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            HandleCollision(other);
            Destroy(gameObject);
        }
    }

    protected virtual void HandleCollision(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                HandleHitEffect(other);
                soundManager?.PlayHitSound();
                enemy.TakeDamage(damage);
            }
        }
    }

    

    protected virtual void HandleHitEffect(Collision2D taget)
    {
        if ( hitEffect !=null)
        {
            GameObject newEffect = Instantiate(hitEffect, taget.gameObject.transform.position, Quaternion.identity);
        }
    }
    
    

    public virtual void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}