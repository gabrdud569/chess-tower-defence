using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] private BoardController boardController;
    [SerializeField] private EnemySpawner enemySpawner;

    private void Start()
    {
        boardController.Init();
        enemySpawner.Init();
    }
}
