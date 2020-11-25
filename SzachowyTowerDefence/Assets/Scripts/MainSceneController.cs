using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main Scene Controller - initializes all managers and controller
/// </summary>
public class MainSceneController : MonoBehaviour
{
    [SerializeField] private BoardController boardController;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private ClickDetectController clickDetectController;
    [SerializeField] private FiguresManager figuresManager;
    [SerializeField] private PointValuesController pointValuesController;
    [SerializeField] private CurrentLevelController levelController;


    /// <summary>
    /// Creates controllers, spawners and managers
    /// </summary>
    private void Start()
    {
        levelController.Init();
        pointValuesController.Init(levelController, levelController.StartPoints);
        figuresManager.Init(pointValuesController);
        boardController.Init(figuresManager);
        enemySpawner.Init(pointValuesController, levelController);
        clickDetectController.Init(boardController, figuresManager);
    }
}
