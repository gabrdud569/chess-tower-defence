using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private List<BoardPointController> boardPointControllers;
    [SerializeField] private PathConfig pathConfig;

    //public void Start()
    //{
    //    foreach (var item in boardPointControllers)
    //    {
    //        item.ChangeState(BoardPointType.Free);
    //        item.SetVisible(false);
    //    }
    //}

    public List<PathElement> GetPath()
    {
        foreach (var item in boardPointControllers)
        {
            item.ChangeState(BoardPointType.Free);
            item.SetVisible(true);
        }

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
}
