using UnityEngine;

public abstract class SwordBase : MonoBehaviour
{
    [SerializeField] protected float speed = 2f;
    [SerializeField] protected int damage = 10;

    // private BulletSoundManager soundManager;
    
    public virtual void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
    public virtual void SetSpeed(int newSpeed)
    {
        speed = newSpeed;
    }
}