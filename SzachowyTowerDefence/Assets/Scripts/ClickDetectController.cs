﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Click Detect Controller - controls mouse detection
/// </summary>
public class ClickDetectController : MonoBehaviour
{
    private BoardController boardController;
    private FiguresManager figuresManager;

    public void Init(BoardController boardController, FiguresManager figuresManager)
    {
        this.boardController = boardController;
        this.figuresManager = figuresManager;
    }

    /// <summary>
    /// Detects mouse click and invokes dedicaated controllers
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                boardController.OnClickDetected(hit.transform.name);
                figuresManager.OnClickDetected(hit.transform.name);
            }
        }
    }
}
