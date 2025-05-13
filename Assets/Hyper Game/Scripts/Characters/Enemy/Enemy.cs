using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{
    private GameObject player;
    private EnemyMovementBase enemyMovement;
    [SerializeField] GameObject itemBonus;
    [SerializeField] ScoreEntry scoreEntry;
    private float attackCooldown = 1f; // Thời gian delay giữa các đòn tấn công
    private float lastAttackTime = 0;
    private Coroutine attackRoutine;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovementBase>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            enemyMovement.IsMoving(false);
            attackRoutine = StartCoroutine(AttackCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // EnemyAttackController.Instance.UnregisterEnemy(this);
            enemyMovement.IsMoving(true);
            if (attackRoutine != null)
            {
                StopCoroutine(attackRoutine);
                attackRoutine = null; // Ensure coroutine reference is cleared
            }
            player = null;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        // Nếu kẻ địch vừa tấn công xong, chờ đủ thời gian hồi chiêu
        float waitTime = lastAttackTime + attackCooldown - Time.time;
        if (waitTime > 0)
        {
            yield return new WaitForSeconds(waitTime);
        }

        while (player != null)
        {
            lastAttackTime = Time.time; 
            Attack();
            yield return new WaitForSeconds(attackCooldown);
        }
        attackRoutine = null;
    }

    public void Attack()
    {
        player.GetComponent<Character>().TakeDamage(GetDamage());
        enemyMovement.Attack();
    }
    public int GetDamage()
    {
        return damage;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    protected override void Die()
    {
        ScoreEvent.RaiseScore(scoreEntry); // 🔥 Gửi sự kiện khi enemy chết
        // DisablePhysics(); // 🔥 Gọi phương thức vô hiệu hóa vật lý
        

        enemyMovement.Die();
        
        if (itemBonus)
        {
            Instantiate(itemBonus, transform.position, transform.rotation);
        }
        StartCoroutine(DestroyAfterDelay(1f));
    }

    // 🕒 Coroutine để delay việc xóa enemy
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Chờ 1 giây
        Destroy(gameObject); // Xóa enemy khỏi scene
    }
    
}
