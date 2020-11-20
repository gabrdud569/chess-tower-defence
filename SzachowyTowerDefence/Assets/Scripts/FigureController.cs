using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureController : MonoBehaviour
{
    [SerializeField] private int shotingRange;
    [SerializeField] private int bulletsPerShot;

    public bool IsMoving => isMoving;

    private bool isMoving = false;
    private bool onBoard = false;
    private bool readyForShoting = false;
    private Vector3 endPosition;
    private Action<FigureController, Vector3, int> onMoveEnded;
    private AnimationCurve curve;
    private float fullDistance;
    private Vector3 startPosition;

    private List<BoardPointController> boardPointsInRange;
    private List<BoardPointController> boardPointsInRangeInPath;

    public bool StartMoveFigure(Vector3 endPosition, Action<FigureController, Vector3, int> onMoveEnded, AnimationCurve yCurve)
    {
        if (!isMoving && !onBoard)
        {
            this.startPosition = gameObject.transform.position;
            this.onMoveEnded = onMoveEnded;
            this.endPosition = endPosition;
            this.curve = yCurve;
            fullDistance = Mathf.Sqrt(Mathf.Pow(gameObject.transform.position.x - endPosition.x, 2) + Mathf.Pow(gameObject.transform.position.z - endPosition.z, 2));
            isMoving = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetPointsForShoting(List<BoardPointController> boardPointsInRange, List<BoardPointController> boardPointsInRangeInPath)
    {
        this.boardPointsInRange = boardPointsInRange;
        this.boardPointsInRangeInPath = boardPointsInRangeInPath;
        readyForShoting = true;
        StartCoroutine(Shot());
    }

    private void FixedUpdate()
    {
        MovingToBoardAnimation();
    }

    private IEnumerator Shot()
    {
        while (readyForShoting)
        {
            List<OpponentController> opponentsToShot = new List<OpponentController>();

            for (int i = 0; i < bulletsPerShot; i++)
            {
                OpponentController newOpponent = GetNextOpponentToShot(opponentsToShot);

                if (newOpponent != null)
                {
                    opponentsToShot.Add(newOpponent);
                }
                else
                {
                    break;
                }
            }

            foreach (var opponent in opponentsToShot)
            {
                GameObject bullet = Instantiate(Resources.Load<GameObject>("BlueBullet"));
                bullet.transform.position = this.transform.position;
                bullet.AddComponent<BulletController>();
                bullet.AddComponent<BoxCollider>().isTrigger = true;
                bullet.GetComponent<BulletController>().Initialize(Resources.Load<BulletConfig>("BulletConfig"), opponent);
            }

            float frames = Time.fixedDeltaTime * 10;
            yield return new WaitForSeconds(frames);
        }
    }

    private OpponentController GetNextOpponentToShot(List<OpponentController> opponentsToShot)
    {
        foreach (var point in boardPointsInRangeInPath)
        {
            foreach (var opponent in point.OpponentControllers)
            {
                if (!opponentsToShot.Contains(opponent))
                {
                    return opponent;
                }
            }
        }

        return null;
    }

    private void MovingToBoardAnimation()
    {
        if (isMoving)
        {
            if (gameObject.transform.position == endPosition)
            {
                onBoard = true;
                isMoving = false;
                onMoveEnded(this, startPosition, shotingRange);
            }
            else
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
                    new Vector3(endPosition.x, endPosition.y + 20 * curve.Evaluate(1 - Mathf.Sqrt(Mathf.Pow(gameObject.transform.position.x - endPosition.x, 2)
                    + Mathf.Pow(gameObject.transform.position.z - endPosition.z, 2)) / fullDistance), endPosition.z), Time.fixedDeltaTime * 6);
            }
        }
    }
}
