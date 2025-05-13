using UnityEngine;
using System.Linq;

public class EnemyTracker : MonoBehaviour
{
    public Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.OrderBy(e => (transform.position - e.transform.position).sqrMagnitude)
                      .FirstOrDefault()?.transform;
    }
}