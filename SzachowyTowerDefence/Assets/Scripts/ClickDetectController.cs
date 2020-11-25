using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetectController : MonoBehaviour
{
    private BoardController boardController;
    private FiguresManager figuresManager;

    public void Init(BoardController boardController, FiguresManager figuresManager)
    {
        this.boardController = boardController;
        this.figuresManager = figuresManager;
    }

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
                }
            }
        }
    }
}
