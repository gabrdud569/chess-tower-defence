using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Figures Manager - controls chess pieces creation on scene
/// </summary>
public class FiguresManager : MonoBehaviour
{
    [SerializeField] private List<FigureController> figures;
    [SerializeField] private AnimationCurve curve;

    public event Action<FigureController, int> OnFigureOnBoardAction = delegate { };

    private PointValuesController pointValuesController;
    private FigureController figureToPlace;
    private List<string> figuresNames;

    public void Init(PointValuesController pointValuesController)
    {
        this.pointValuesController = pointValuesController;
        figuresNames = new List<string>();
        figures.ForEach(x => figuresNames.Add(x.name));
    }

    /// <summary>
    /// Gets new figure to place
    /// </summary>
    /// <returns></returns>
    public FigureController GetFigureToPlace()
    {
        return figureToPlace;
    }

    /// <summary>
    /// Invoked after clicking o chess piece detection, highlights clicked piece
    /// </summary>
    /// <param name="objectName">Clicked object</param>
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

    /// <summary>
    /// Moves chess piece to given tile
    /// </summary>
    /// <param name="endPosition">End of moving position</param>
    /// <param name="point">Tile on map</param>
    /// <param name="onSuccesFigureMoving">Callback to succesful figure movement</param>
    public void OnPointOnBoardClicked(Vector3 endPosition, BoardPointController point, Action<BoardPointController> onSuccesFigureMoving)
    {
        if (figureToPlace != null)
        {
            if(figureToPlace.CanMoveFigure() && pointValuesController.RemovePoints(figureToPlace.FigureCost))
            {
                figureToPlace.StartMoveFigure(endPosition, OnFigureOnBoard, curve);
                onSuccesFigureMoving(point);
            }

            figureToPlace = null;
        }
    }

    /// <summary>
    /// Invoked after cess piece was placed on board
    /// </summary>
    /// <param name="controller">Chess pieces controller</param>
    /// <param name="startPosition">Position of piece</param>
    /// <param name="figureRange">Range of attack</param>
    private void OnFigureOnBoard(FigureController controller, Vector3 startPosition, int figureRange)
    {
        if (figures.Contains(controller))
        {
            OnFigureOnBoardAction(controller, figureRange);
            SpawnNewFigure(controller, startPosition);
        }
    }

    /// <summary>
    /// Spawns new figure on scene
    /// </summary>
    /// <param name="controller">Chess piece controller</param>
    /// <param name="startPosition">Piece starting position</param>
    private void SpawnNewFigure(FigureController controller, Vector3 startPosition)
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