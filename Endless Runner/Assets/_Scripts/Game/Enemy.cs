using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Spawnables")]
    public GameObject Bullet;
    [Space]
    public float minBulletSpawnTime;
    public float maxBulletSpawnTime;
    [Space]
    float bulletSpawnTime;
    public float lifeTime;

    void Start()
    {
        bulletSpawnTime = minBulletSpawnTime;                 
    }

    void Update()
    {
        SelfDestruct();
        ShootBullet();
    }
    void ShootBullet()
    {
        bulletSpawnTime -= Time.deltaTime;
        if (bulletSpawnTime <= 0)
        {
            Instantiate(Bullet, transform.position, transform.rotation);
            float spawnTime = Random.Range(minBulletSpawnTime, maxBulletSpawnTime);
            bulletSpawnTime = spawnTime;
        }
    }

    void SelfDestruct()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }


}
