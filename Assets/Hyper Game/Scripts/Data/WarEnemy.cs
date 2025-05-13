using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WarEnemy
{
    public List<GameObject> enemies;
    public int timeout;

    public WarEnemy(int timeout, List<GameObject> enemies)
    {
        this.timeout = timeout;
        this.enemies = enemies;
    }
}