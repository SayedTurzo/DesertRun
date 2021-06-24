using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    Rigidbody BulletRigid;

    void Start()
    {
        BulletRigid = GetComponent<Rigidbody>();
        GlobalData.bulletSpeed = speed;
    }

    void Update()
    {
        MoveBullet();
        DestroyBullet();
    }

    void MoveBullet()
    {
        BulletRigid.velocity = new Vector3(speed, 0, 0);
    }

    void DestroyBullet()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision obj)
    {
        if (gameObject.CompareTag("Bullet"))
        {
            if (obj.gameObject.CompareTag("Enemy") || obj.gameObject.CompareTag("EnemyBullet"))
            {
                Destroy(obj.gameObject);
                Destroy(gameObject);

                if (obj.gameObject.CompareTag("Enemy"))
                {
                    GlobalData.gameScore += GlobalData.enemyScoreIncrease;
                }
            }
        }
        if (gameObject.CompareTag("EnemyBullet"))
        {
            if (obj.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
