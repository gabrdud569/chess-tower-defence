using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] private BoardController boardController;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private ClickDetectController clickDetectController;
    [SerializeField] private FiguresManager figuresManager;
    [SerializeField] private PointValuesController pointValuesController;
    [SerializeField] private CurrentLevelController levelController;

    [SerializeField] private Camera tabletCamera;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Button scanFiguresButton;
    [SerializeField] private Button backFromScanFiguresButton;

    private void Start()
    {
        levelController.Init();
        pointValuesController.Init(levelController, levelController.StartPoints);
        figuresManager.Init(pointValuesController);
        boardController.Init(figuresManager);
        enemySpawner.Init(pointValuesController, levelController);
        clickDetectController.Init(boardController, figuresManager);
    }

    public void OnOpenTablet()
    {
        backFromScanFiguresButton.gameObject.SetActive(true);
        scanFiguresButton.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(false);
        tabletCamera.gameObject.SetActive(true);
    }

    public void OnCloseTabled()
    {
        backFromScanFiguresButton.gameObject.SetActive(false);
        scanFiguresButton.gameObject.SetActive(true);
        tabletCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
    }
}
