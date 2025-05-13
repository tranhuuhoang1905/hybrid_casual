using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float health, maxHealth = 3f;
    [SerializeField] protected int damage = 10;
    private bool isDie = false;
    protected SliderBar healthBar;
    private IHitEffect hitEffect;

    protected virtual void Awake()
    {
        healthBar = GetComponentInChildren<SliderBar>();
        hitEffect = GetComponent<IHitEffect>();
    }
    void Start()
    {
        Transform healthBarTransform = transform.Find("Canvas");
    }
    protected virtual void Initialize()
    {
        // Phương thức này có thể được ghi đè bởi các lớp con để khởi tạo các thuộc tính riêng
    }

    public virtual void TakeDamage(int damageAmount)
    {
        if (isDie) return;
        health -= damageAmount;
        healthBar.UpdateSliderBar(health, maxHealth);
        Vector3 positionShow = (healthBar.transform.position + transform.position) / 2;
        GameEvents.ShowFloatingText(positionShow, damageAmount,FloatingType.ExceptBlood);
        if (hitEffect != null)
        {
            hitEffect.ApplyHitEffect();
        }
        if (health <= 0)
        {
            isDie = true;
            DisablePhysics();
            Die();
        }
    }
    public virtual void ApplyBurnEffect(float burnDamageOverTime)
    {
        // Xử lý hiệu ứng cháy
    }

    public virtual void ApplySlowEffect(float slowEffectDuration)
    {
        // Xử lý hiệu ứng làm chậm
    }
    protected abstract void Die();
    private void DisablePhysics()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.isKinematic = true; // Ngăn enemy bị rơi
            rb.velocity = Vector3.zero; // Dừng mọi chuyển động
        }
        Collider2D col = GetComponent<Collider2D>();
        if (col)
        {
            col.enabled = false; // Vô hiệu hóa va chạm
        }
    }
}
