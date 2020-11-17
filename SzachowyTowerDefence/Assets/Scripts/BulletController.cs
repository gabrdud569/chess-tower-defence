using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public delegate Vector3 GetEnemyPosition();
    public delegate void DealDamage(int damage);
    public delegate bool IsAlive();

    public GetEnemyPosition getEnemyPosition;
    public DealDamage dealDamage;
    public IsAlive isAlive;

    private int damage;
    private float speed;
    private string enemyName;
    private BulletConfig bulletConfig;

    public void Initialize(BulletConfig config, string enemyName, GetEnemyPosition getEnemyPosition, DealDamage dealDamage, IsAlive isAlive)
    {
        this.isAlive = isAlive;
        this.dealDamage = dealDamage;
        this.getEnemyPosition = getEnemyPosition;
        this.enemyName = enemyName;
        damage = config.damage;
        speed = config.speed;

    }

    void FixedUpdate()
    {
        if (isAlive())
        {
            Vector3 enemyPosition = getEnemyPosition();
            Vector3 direction = enemyPosition - transform.position;
            direction.Normalize();
            transform.Translate(direction * speed * Time.fixedDeltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == enemyName)
        {
            dealDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
