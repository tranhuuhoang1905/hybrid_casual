using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.FantasyMonsters.Common.Scripts;

public class EnemyMovementBase : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 1f;
    protected Animator myAnimator;
    private SpriteRenderer spriteRenderer;
    protected bool isMovement = true;
    protected bool isDie = false;
    protected Transform player;
    protected Monster monster;
    protected GameObject bodyMonster;

    protected virtual void Start()
    {
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        monster = GetComponent<Monster>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        bodyMonster = transform.Find("Body")?.gameObject;

        
    }

    protected virtual void Update()
    {
        if (isDie) return;
        if (!isMovement ) return;
        Move();
    }

    protected virtual void Move()
    {
        Vector2 direction = (player.position - transform.position ).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        FlipEnemyFacing(direction.x);
    }

    protected virtual void FlipEnemyFacing(float directionX)
    {
        if (directionX == 0 ) return;
        if (bodyMonster != null)
            {
                if (directionX < 0)
                {
                    monster.Flip(1);
                }
                else
                {
                    monster.Flip(-1);
                }
                
            }
            else
            {
                
                spriteRenderer.flipX = directionX < 0; // Flip chỉ sprite
            }
    }

    public virtual void IsMoving(bool action)
    {
        isMovement = action;
    }
    public virtual void Attack()
    {
        if (monster)
        {
            monster.Attack();
        }
    }
    public virtual void Die()
    {
        isDie = true;
        if (monster)
        {
            monster.Die();
        }
    }
}
