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
}
