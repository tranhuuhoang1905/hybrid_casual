using UnityEngine;
using System.Collections;

public class CharacterFireHandler : CharacterWeaponHandler
{
    private BulletSystem bulletSystem;
    private CharacterMovement characterMovement;
    private EnemyTracker enemyTracker;
    private Animator myAnimator;
    [SerializeField] private GameObject auraEffect;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
        bulletSystem = GetComponent<BulletSystem>();
        enemyTracker = GetComponent<EnemyTracker>();

        InvokeRepeating("AutoFire", 1f, 1f);
    }

    protected void spawnAuraEffect()
    {
        if ( auraEffect !=null)
        {
            GameObject newEffect = Instantiate(auraEffect,transform.position, Quaternion.identity);
        }
    }

    public void AutoFire()
    {
        if (!characterMovement.IsAlive) return;
    
        myAnimator.SetTrigger("IsActack");
        Invoke("InstantiateBullet", 0.2f);
    }

    void InstantiateBullet()
    {
        
        float direction = Mathf.Sign(transform.localScale.x);
        Quaternion baseRotation = direction > 0 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);

        Transform enemy =  GetClosestEnemy();
        if (enemy == null)
        {
            return;
        }
        Vector3 enemyPosition =  enemy.position;
        enemyPosition.z = 0;
        
        Vector2 directionToMouse = (enemyPosition - bulletSystem.transform.position).normalized;
        float baseAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        

        // Xác định số viên đạn bắn ra theo level
        int bulletsToShoot = level; // Mỗi level tăng 1 viên, level 1 bắn 1 viên

        float spreadAngle = 10f; // Góc lệch giữa các viên đạn
        float startAngle = baseAngle - ((bulletsToShoot - 1) * spreadAngle / 2); // Cân bằng góc bắn
        
        if (bulletsToShoot <1) return;
        spawnAuraEffect();
        for (int i = 0; i < bulletsToShoot; i++)
        {
            float angle = startAngle + (i * spreadAngle);
            Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);

            // Bắn viên đạn
            bulletSystem.SpawnBullet(bulletRotation);
        }
    }

    Transform GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }

        return closest;
    }

    private Vector3 FindPositionAttack(Transform enemy){
        Transform healthBar = enemy.transform.Find("Canvas");
        if (!healthBar) return enemy.transform.position;
        Vector3 positionAttack = enemy.transform.position;
        positionAttack.y = (healthBar.transform.position.y + enemy.transform.position.y) / 2;        
        return positionAttack;
    }


    void SpantSpeedRefresh(Attr totalStats)
    {
        float newFireRate = 1 / totalStats.attackSpeed;
        bulletSystem.UpdateFireRate(newFireRate);
    }
}
