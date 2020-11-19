using System.Collections.Generic;
using UnityEngine;

public class FiguresManager : MonoBehaviour
{
    [SerializeField] List<FigureController> figures;

    private FigureController figureToPlace;

    public FigureController GetFigureToPlace()
    {
        return figureToPlace;
    }

    public void OnClickDetected(string objectName)
    {
        if (figures.Exists(x => x.name == objectName))
        {
            Material standardMaterial = Resources.Load<Material>("Light");
            figures.ForEach((x) => x.GetComponent<MeshRenderer>().sharedMaterial = standardMaterial);
            figureToPlace = figures.Find(x => x.name == objectName);
            figureToPlace.GetComponent<MeshRenderer>().sharedMaterial = Resources.Load<Material>("HightlightLight");
        }
    }

    public void OnPointOnBoardClicked(Vector3 endPosition)
    {
        if(figureToPlace != null)
        {
            figureToPlace.StartMoveFigure(endPosition);
            figureToPlace = null;
        }
    }
}