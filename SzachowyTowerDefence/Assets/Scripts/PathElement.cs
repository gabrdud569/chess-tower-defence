using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Path element - controls enemies on path
/// </summary>
public class PathElement : MonoBehaviour
{
    [SerializeField] private EnemyControlPointType enemyControlPoint;

    private List<OpponentController> opponentsOnThisPoint = new List<OpponentController>();

    public List<OpponentController> OpponentControllers => opponentsOnThisPoint;

    /// <summary>
    /// Retruns path transform
    /// </summary>
    /// <returns></returns>
    public Transform GetTransform()
    {
        return this.transform;
    }

    /// <summary>
    /// Removes opponentController from opponents on this point
    /// </summary>
    /// <param name="opponent">Opponent leaving this tile</param>
    public void LeaveFromThisPoint(OpponentController opponent)
    {
        if (opponentsOnThisPoint.Contains(opponent))
        {
            opponentsOnThisPoint.Remove(opponent);
        }
    }

    /// <summary>
    /// Detects collision with enemy
    /// </summary>
    /// <param name="other">Opponent which movement </param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<OpponentController>() != null)
        {
            other.GetComponent<OpponentController>().OnPointInPathEntered(this);
            other.GetComponent<OpponentController>().OnPointInPathEntered(this);

            if (enemyControlPoint == EnemyControlPointType.StartPoint || enemyControlPoint == EnemyControlPointType.OnBoard)
            {
                opponentsOnThisPoint.Add(other.GetComponent<OpponentController>());

                if (enemyControlPoint == EnemyControlPointType.StartPoint)
                {
                    other.GetComponent<OpponentController>().OnStartMoveOnBoard();
                }
            }
            else if (enemyControlPoint == EnemyControlPointType.EndPoint)
            {
                other.GetComponent<OpponentController>().OnEndPointAchieved();
            }
        }
    }
}
