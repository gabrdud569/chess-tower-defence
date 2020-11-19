using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguresManager : MonoBehaviour
{
    public static FiguresManager instance;

    void Awake ()
    {
        if (instance !=null)
        { 
            Debug.LogError("More than one FiguresManager!")
        }
        instance = this;
    }

    private GameObject figureToPlace;

    public GameObject GetFigureToPlace()
    {
        return figureToPlace;
    }


}
