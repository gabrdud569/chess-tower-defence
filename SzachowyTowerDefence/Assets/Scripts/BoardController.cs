using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private List<BoardPointController> boardPointControllers;
    [SerializeField] private PathConfig pathConfig;

    public List<PathElement> GetPath()
    {
        List<PathElement> path = new List<PathElement>();

        foreach (string pathElement in pathConfig.path)
        {
            path.Add(boardPointControllers.Find(x => x.name.Contains(pathElement)));
        }

        return path;
    }
}
