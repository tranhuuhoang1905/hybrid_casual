using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : SwordBase
{

    private SwordMovement movement;

    void Awake()
    {
        movement = GetComponent<SwordMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();
            enemy.TakeDamage(damage);
        }
    }
    public void RefreshSwordStats( Attr totalStats)
    {
        damage = totalStats.swordAttack;
        speed = totalStats.swordSpeed;
        movement.updateSpeed(speed);
    }
}
