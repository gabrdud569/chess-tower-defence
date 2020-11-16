using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<OpponentConfig> opponentConfig;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private PathProvider pathProvider;
    [SerializeField] private BoardController boardController;

    private List<PathElement> path;

    void Start()
    {
        path = pathProvider.StartPathElements;
        path.AddRange(boardController.GetPath());
        path.AddRange(pathProvider.EndPathElements);
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        for (int j = 0; j < 50; j++)
        {
            int randomValue = Random.Range(0, 3);
            int i = 10;

            while (i-- > 0)
            {
                yield return new WaitForSeconds(0.1f);
                GameObject enemyPrefab = Instantiate(enemyPrefabs[randomValue]);
                enemyPrefab.transform.position = this.transform.position;
                enemyPrefab.GetComponent<OpponentController>().Initialize(path, opponentConfig[randomValue]);
            }
        }
    }
}
