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

    public void OnPointOnBoardClicked(Vector3 endPosition)
    {
        if (figureToPlace != null)
        {
            figureToPlace.StartMoveFigure(endPosition, OnFigureOnBoard);
            figureToPlace = null;
        }
    }

    private void OnFigureOnBoard(FigureController controller)
    {
        Material standardMaterial = Resources.Load<Material>("Light");
        controller.GetComponent<MeshRenderer>().sharedMaterial = standardMaterial;
    }
}