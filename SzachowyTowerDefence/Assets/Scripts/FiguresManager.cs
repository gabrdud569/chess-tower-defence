using System;
using System.Collections.Generic;
using UnityEngine;

public class FiguresManager : MonoBehaviour
{
    [SerializeField] private List<FigureController> figures;
    [SerializeField] private AnimationCurve curve;
    
    private FigureController figureToPlace;
    private List<string> figuresNames;

    public void Init()
    {
        figuresNames = new List<string>();
        figures.ForEach(x => figuresNames.Add(x.name));
    }

    public FigureController GetFigureToPlace()
    {
        return figureToPlace;
    }

    public void OnClickDetected(string objectName)
    {
        Material standardMaterial = Resources.Load<Material>("Light");
        figures.ForEach((x) =>
        {
            if (!x.IsMoving)
            {
                x.GetComponent<MeshRenderer>().sharedMaterial = standardMaterial;
            };
        });

        if (figures.Exists(x => x.name == objectName) && !figures.Exists(x => x.IsMoving))
        {
            figureToPlace = figures.Find(x => x.name == objectName);
            figureToPlace.GetComponent<MeshRenderer>().sharedMaterial = Resources.Load<Material>("HightlightLight");
        }
    }

    public void OnPointOnBoardClicked(Vector3 endPosition, BoardPointController point, Action<BoardPointController> onSuccesFigureMoving)
    {
        if (figureToPlace != null)
        {
            if(figureToPlace.StartMoveFigure(endPosition, OnFigureOnBoard, curve))
            {
                onSuccesFigureMoving(point);
            }

            figureToPlace = null;
        }
    }

    private void OnFigureOnBoard(FigureController controller, Vector3 startPosition)
    {
        if (figures.Contains(controller))
        {
            Material standardMaterial = Resources.Load<Material>("Light");
            controller.GetComponent<MeshRenderer>().sharedMaterial = standardMaterial;

            string prefabName = figuresNames.Find(x => controller.name.Contains(x));
            GameObject newFigure = Instantiate(Resources.Load<GameObject>(prefabName));
            newFigure.name = prefabName + figures.Count.ToString();
            newFigure.transform.position = startPosition;
            newFigure.transform.SetParent(this.gameObject.transform);

            figures.Add(newFigure.GetComponent<FigureController>());
        }
    }
}