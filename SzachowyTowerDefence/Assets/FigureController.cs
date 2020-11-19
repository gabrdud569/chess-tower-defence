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
    private Vector3 vectorToMove;
    private Action<FigureController> onMoveEnded;

    public void StartMoveFigure(Vector3 endPosition, Action<FigureController> onMoveEnded)
    {
        if (!isMoving && !onBoard)
        {
            this.onMoveEnded = onMoveEnded;
            isMoving = true;
            this.endPosition = endPosition;
            vectorToMove = endPosition.normalized - gameObject.transform.position.normalized;
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
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, endPosition, Time.fixedDeltaTime);
            }
        }
    }
}
