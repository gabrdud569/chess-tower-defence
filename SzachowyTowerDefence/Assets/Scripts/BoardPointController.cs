using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPointController : PathElement
{
    public BoardPointType BoardPointType => boardPointType;

    private BoardPointType boardPointType;

    public void SetVisible(bool visible)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(visible);
        }
    }

    public void ChangeState(BoardPointType boardPointType)
    {
        this.boardPointType = boardPointType;
        string materialName = "Highlights";

        switch(boardPointType)
        {
            case BoardPointType.Free:
                materialName += "Free";
                break;
            case BoardPointType.Occupied:
                materialName += "Occupied";
                break;
            case BoardPointType.Path:
                materialName += "Path";
                break;
            case BoardPointType.Range:
                materialName += "Range";
                break;
            default:
                break;
        }

        Material material = Resources.Load<Material>(materialName);

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<MeshRenderer>().sharedMaterial = material;
        }
    }
}
