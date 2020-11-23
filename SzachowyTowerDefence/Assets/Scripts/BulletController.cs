using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private bool isAlive = false;
    private int damage;
    private float speed;
    private string enemyName;
    private OpponentController opponent;

    public void Initialize(BulletConfig config, OpponentController opponent, int damage)
    {
        opponent.OnDead += OnEnemyDead;
        this.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        this.opponent = opponent;
        this.enemyName = opponent.name;
        this.damage = damage;
        speed = config.speed;
        isAlive = true;
    }

    void FixedUpdate()
    {
        if (isAlive && opponent != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, opponent.transform.position, speed * Time.fixedDeltaTime);
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
            other.GetComponent<OpponentController>().DealDamage(damage);
            Destroy(this.gameObject);
        }
    }

    public void OnEnemyDead(int reward)
    {
        isAlive = false;
    }
}
