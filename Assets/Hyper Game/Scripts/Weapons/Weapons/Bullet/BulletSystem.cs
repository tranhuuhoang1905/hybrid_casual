using System.Collections.Generic;
using UnityEngine;

public class BulletSystem : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gun;
    private Queue<GameObject> bulletPool = new Queue<GameObject>(); // Object Pool
    private float fireRate = 1f;

    private Character playerCharacter;

    void Start()
    {
        playerCharacter = GetComponent<Character>();
    }

    public void SpawnBullet(Quaternion bulletRotation)
    {
        GameObject newBullet;
        if (bulletPool.Count > 0)
        {
            newBullet = bulletPool.Dequeue();
            newBullet.SetActive(true);
        }
        else
        {
            newBullet = Instantiate(bulletPrefab);
        }

        newBullet.transform.position = gun.position;
        newBullet.transform.rotation = bulletRotation;

        Bullet bulletScript = newBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(playerCharacter.GetDamage());
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    public void UpdateFireRate(float newRate)
    {
        fireRate = newRate;
        CancelInvoke("AutoFire");
        InvokeRepeating("AutoFire", 0f, fireRate);
    }
}
