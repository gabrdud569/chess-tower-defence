using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Click Detect Controller - controls mouse detection
/// </summary>
public class ClickDetectController : MonoBehaviour
{
    private BoardController boardController;
    private FiguresManager figuresManager;
    [SerializeField] private testOpenCV tablet;

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
            if (Camera.main != null && Camera.main.name == "Main Camera")
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    boardController.OnClickDetected(hit.transform.name);
                    figuresManager.OnClickDetected(hit.transform.name);
                    //tablet.OnClickDetected(hit.transform.name);
                }
            }
        }
    }
}
