using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] private BoardController boardController;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private ClickDetectController clickDetectController;
    [SerializeField] private FiguresManager figuresManager;

    private void Start()
    {
        figuresManager.Init();
        boardController.Init(figuresManager);
        enemySpawner.Init();
        clickDetectController.Init(boardController, figuresManager);
    }
}
