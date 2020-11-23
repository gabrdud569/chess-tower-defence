using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void OnClickDetected(string objectName)
    {
        BoardPointController point = boardPointControllers.Find(x => x.name == objectName);

        if(point != null && point.BoardPointType != BoardPointType.Occupied && point.BoardPointType != BoardPointType.Path)
        {
            figuresManager.OnPointOnBoardClicked(point.transform.position, point, OnPointStartOccupied);
        }
    }

    public void OnPointStartOccupied(BoardPointController point)
    {
        point.ChangeState(BoardPointType.Occupied);
    }

    public void OnFigureEnteredOnBoard(FigureController figure, int figureRange)
    {
        BoardPointController figurePoint = boardPointControllers.Find(x => x.GetTransform().position == figure.transform.position);
        List<BoardPointController> pointsInRange;
        List<BoardPointController> pointsInRangeInPath;
        (pointsInRange, pointsInRangeInPath) = GetPointsInRange(figure.transform.position, figureRange);
        figure.SetPointsForShoting(pointsInRange, pointsInRangeInPath);
    }

    private (List<BoardPointController>, List<BoardPointController>) GetPointsInRange(Vector3 figurePosition, int figureRange)
    {
        List<BoardPointController> pointsInRange = new List<BoardPointController>();
        List<BoardPointController> pointsInRangePath = new List<BoardPointController>();

        foreach (var point in boardPointControllers)
        {
            Vector3 position = point.transform.position;

            if((Mathf.Abs(figurePosition.x - position.x) -0.5f < (float)figureRange)
                && (Mathf.Abs(figurePosition.z - position.z) - 0.5f < (float)figureRange))
            {
                pointsInRange.Add(point);

                if(point.BoardPointType == BoardPointType.Path)
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
