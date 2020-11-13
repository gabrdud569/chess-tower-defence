using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private PathProvider pathProvider;
    [SerializeField] private OpponentConfig opponentConfig;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }


    void Update()
    {
        
    }

    private IEnumerator SpawnEnemy()
    {
        int i = 10;
        while (i-- > 0)
        {
            yield return new WaitForSeconds(2f);
            GameObject enemyPrefab = Instantiate(enemyPrefabs[0]);
            enemyPrefab.transform.position = this.transform.position;
            enemyPrefab.GetComponent<OpponentController>().Initialize(pathProvider.pathElements, opponentConfig);
        }
    }
}
