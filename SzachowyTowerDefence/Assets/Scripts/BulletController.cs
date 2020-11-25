using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bullet Controller - controlls bullet motion
/// </summary>
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

    /// <summary>
    /// Moves bullet into enemy direction
    /// </summary>
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

    /// <summary>
    /// Detects collision with enemy and deals damange
    /// </summary>
    /// <param name="other">Enemy</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == enemyName)
        {
            other.GetComponent<OpponentController>().DealDamage(damage);
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Inoved when enemy dies
    /// </summary>
    /// <param name="reward">Reward</param>
    public void OnEnemyDead(int reward)
    {
        isAlive = false;
    }
}
