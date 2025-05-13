using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveToCenter : EnemyMovementBase
{
    protected override void Move()
    {
        Vector2 direction = (player.position - transform.position ).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        FlipEnemyFacing(direction.x);
    }
}
