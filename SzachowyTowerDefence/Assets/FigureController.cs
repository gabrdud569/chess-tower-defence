using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureController : MonoBehaviour
{
    public bool IsMoving => isMoving;

    private bool isMoving = false;
    private bool onBoard = false;
    private Vector3 endPosition;
    private Action<FigureController> onMoveEnded;
    private AnimationCurve curve;
    private float fullDistance;

    public void StartMoveFigure(Vector3 endPosition, Action<FigureController> onMoveEnded, AnimationCurve yCurve)
    {
        if (!isMoving && !onBoard)
        {
            this.onMoveEnded = onMoveEnded;
            this.endPosition = endPosition;
            this.curve = yCurve;
            fullDistance = Mathf.Sqrt(Mathf.Pow(gameObject.transform.position.x - endPosition.x, 2) + Mathf.Pow(gameObject.transform.position.z - endPosition.z, 2));
            isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            if (gameObject.transform.position == endPosition)
            {
                onBoard = true;
                isMoving = false;
                onMoveEnded(this);
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
