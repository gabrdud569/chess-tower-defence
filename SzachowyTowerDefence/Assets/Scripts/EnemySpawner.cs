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
    private PointValuesController pointValuesController;
    private CurrentLevelController currentLevelController;

    public void Init(PointValuesController pointValuesController, CurrentLevelController currentLevelController)
    {
        this.pointValuesController = pointValuesController;
        this.currentLevelController = currentLevelController;
        path = pathProvider.StartPathElements;
        path.AddRange(boardController.GetPath());
        path.AddRange(pathProvider.EndPathElements);
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        int stageMultiplier = 1;

        while (true)
        {
            for (int j = 0; j < 3; j++)
            {
                int i = 2;

                while (i-- > 0)
                {
                    yield return new WaitForSeconds(1f);
                    GameObject enemyPrefab = Instantiate(enemyPrefabs[j]);
                    enemyPrefab.name = System.Guid.NewGuid().ToString();
                    enemyPrefab.transform.position = this.transform.position;
                    enemyPrefab.transform.SetParent(this.gameObject.transform);
                    enemyPrefab.GetComponent<OpponentController>().OnDead += OnEnemyDead;
                    enemyPrefab.GetComponent<OpponentController>().OnDamageDealed += OnEnemyEndPath;
                    enemyPrefab.GetComponent<OpponentController>().Initialize(path, opponentConfig[j], stageMultiplier);
                }
            }

            stageMultiplier++;
        }
    }

    private void OnEnemyDead(int reward)
    {
        pointValuesController.AddPoints(reward);
    }

    private void OnEnemyEndPath(int hpToRemove)
    {
        currentLevelController.RemoveHp(hpToRemove);
    }
}
