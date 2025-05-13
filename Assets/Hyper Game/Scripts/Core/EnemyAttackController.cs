using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    public static EnemyAttackController Instance;
    private List<Enemy> activeEnemies = new List<Enemy>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(AttackPlayerRoutine());
    }

    public void RegisterEnemy(Enemy enemy)
    {
        if (!activeEnemies.Contains(enemy))
        {
            activeEnemies.Add(enemy);
        }
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
        }
    }

    private IEnumerator AttackPlayerRoutine()
    {
        while (true)
        {
            foreach (var enemy in activeEnemies)
            {
                Character playerCharacter = enemy.GetPlayer().GetComponent<Character>();
                if (playerCharacter != null)
                {
                    playerCharacter.TakeDamage(enemy.GetDamage());
                }
                enemy.Attack();
            } 
            yield return new WaitForSeconds(1f);
        }
    }
}
