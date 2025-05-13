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

    public void FireSkill()
    {
        if (!characterMovement.IsAlive) return;
        if (GameManager.Instance.GetScore() < 20) return;

        ScoreEntry scoreEntry = new ScoreEntry(ScoreType.Score, -20);
        GameManager.Instance.AddToScore(scoreEntry);

        StartCoroutine(FireMultipleRounds());
    }

    IEnumerator FireMultipleRounds()
    {
        for (int i = 0; i < 3; i++)
        {
            FireBurst();
            yield return new WaitForSeconds(0.2f);
        }
    }

    void FireBurst()
    {
        float angleStep = 360f / 20;
        float startAngle = 0f;
        spawnAuraEffect();
        for (int i = 0; i < 20; i++)
        {
            float angle = startAngle + (angleStep * i);
            Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);
            bulletSystem.SpawnBullet(bulletRotation);
        }
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

        // Lấy vị trí chuột trong thế giới
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Tính hướng từ nhân vật đến chuột
        Vector2 directionToMouse = (mousePosition - bulletSystem.transform.position).normalized;
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
