using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Board Controller - controls chess board
/// </summary>
public class BoardController : MonoBehaviour
{
    [SerializeField] private List<BoardPointController> boardPointControllers;
    [SerializeField] private PathConfig pathConfig;

    private FiguresManager figuresManager;

    public void Init(FiguresManager figuresManager)
    {
        this.figuresManager = figuresManager;
        figuresManager.OnFigureOnBoardAction += OnFigureEnteredOnBoard;

        foreach (var point in boardPointControllers)
        {
            point.ChangeState(BoardPointType.Free);
            point.SetVisible(false);
        }
    }

    /// <summary>
    /// Gets path on board
    /// </summary>
    /// <returns></returns>
    public List<PathElement> GetPath()
    {
        List<PathElement> path = new List<PathElement>();

        foreach (string pathElement in pathConfig.path)
        {
            BoardPointController controller = boardPointControllers.Find(x => x.name.Contains(pathElement));
            controller.ChangeState(BoardPointType.Path);
            controller.SetVisible(true);
            path.Add(controller);
        }

        return path;
    }

    /// <summary>
    /// Invoked after clicking on board
    /// </summary>
    /// <param name="objectName"></param>
    public void OnClickDetected(string objectName)
    {
        BoardPointController point = boardPointControllers.Find(x => x.name == objectName);

        if (point != null && point.BoardPointType != BoardPointType.Occupied && point.BoardPointType != BoardPointType.Path)
        {
            figuresManager.OnPointOnBoardClicked(point.transform.position, point, OnPointStartOccupied);
        }
    }

    /// <summary>
    /// Invoked after tile becoms occupied
    /// </summary>
    /// <param name="point">Tile on board</param>
    public void OnPointStartOccupied(BoardPointController point)
    {
        point.ChangeState(BoardPointType.Occupied);
    }

    /// <summary>
    /// Invoked after chess piece was placed on board
    /// </summary>
    /// <param name="figure">Chess piece controller</param>
    /// <param name="figureRange">Chess piece range</param>
    public void OnFigureEnteredOnBoard(FigureController figure, int figureRange)
    {
        BoardPointController figurePoint = boardPointControllers.Find(x => x.GetTransform().position == figure.transform.position);
        List<BoardPointController> pointsInRange;
        List<BoardPointController> pointsInRangeInPath;
        (pointsInRange, pointsInRangeInPath) = GetPointsInRange(figure.transform.position, figureRange);
        figure.SetPointsForShoting(pointsInRange, pointsInRangeInPath);
    }

    /// <summary>
    /// Returns tiles on board, that are in range of chess piece
    /// </summary>
    /// <param name="figurePosition">Chess piece position</param>
    /// <param name="figureRange">Chesse piece range</param>
    /// <returns></returns>
    private (List<BoardPointController>, List<BoardPointController>) GetPointsInRange(Vector3 figurePosition, int figureRange)
    {
        List<BoardPointController> pointsInRange = new List<BoardPointController>();
        List<BoardPointController> pointsInRangePath = new List<BoardPointController>();

        foreach (var point in boardPointControllers)
        {
            Vector3 position = point.transform.position;

            if ((Mathf.Abs(figurePosition.x - position.x) - 0.5f < (float)figureRange)
                && (Mathf.Abs(figurePosition.z - position.z) - 0.5f < (float)figureRange))
            {
                pointsInRange.Add(point);

                if (point.BoardPointType == BoardPointType.Path)
                {
                    pointsInRangePath.Add(point);
                }
            }
        }

        pointsInRange.Reverse();
        pointsInRangePath.Reverse();
        return (pointsInRange, pointsInRangePath);
    }
}
