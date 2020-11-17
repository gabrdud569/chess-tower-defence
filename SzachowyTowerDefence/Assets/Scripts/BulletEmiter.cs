using System.Collections;
using UnityEngine;

public class BulletEmiter : MonoBehaviour
{

    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private BulletConfig bulletConfig;

    private int totalDamage = 0;
    private bool alive = true;
    private Vector3 getEnemyPosition()
    {
        return enemy.transform.position;
    }

    private bool isAlive()
    {
        return alive;
    }

    private void dealDamage(int damage)
    {
        if(totalDamage > 100)
        {
            alive = false;
            Destroy(enemy.gameObject);
        }
        totalDamage += damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        for (int j = 0; j < 50; j++)
        {
            int randomValue = Random.Range(0, 3);
            int i = 10;

            while (i-- > 0)
            {
                yield return new WaitForSeconds(0.5f);
                if (alive)
                {
                    GameObject bullet = Instantiate(bulletPrefab);
                    bullet.transform.position = this.transform.position;
                    bullet.AddComponent<BulletController>();
                    bullet.AddComponent<BoxCollider>().isTrigger = true;
                    bullet.GetComponent<BulletController>().Initialize(bulletConfig, enemy.name, getEnemyPosition, dealDamage, isAlive);
                }
            }
        }
    }
}
