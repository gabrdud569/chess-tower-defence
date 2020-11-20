using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathElement : MonoBehaviour
{
    [SerializeField] private EnemyControlPointType enemyControlPoint;

    private List<OpponentController> opponentsOnThisPoint = new List<OpponentController>();
    
    public List<OpponentController> OpponentControllers => opponentsOnThisPoint;

    public Transform GetTransform()
    {
        return this.transform;
    }

    public void LeaveFromThisPoint(OpponentController opponent)
    {
        if(opponentsOnThisPoint.Contains(opponent))
        {
            opponentsOnThisPoint.Remove(opponent);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<OpponentController>() != null)
        {
            other.GetComponent<OpponentController>().OnPointInPathEntered(this);

            if(enemyControlPoint == EnemyControlPointType.StartPoint || enemyControlPoint == EnemyControlPointType.OnBoard)
            {
                opponentsOnThisPoint.Add(other.GetComponent<OpponentController>());

                if(enemyControlPoint == EnemyControlPointType.StartPoint)
                {
                    other.GetComponent<OpponentController>().OnStartMoveOnBoard();
                }
            }
            else if(enemyControlPoint == EnemyControlPointType.EndPoint)
            {
                other.GetComponent<OpponentController>().OnEndPointAchieved();
            }
        }
    }
}
