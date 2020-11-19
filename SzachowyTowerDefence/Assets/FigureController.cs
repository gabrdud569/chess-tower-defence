using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureController : MonoBehaviour
{
    bool isMoving = false;
    private Vector3 endPosition;

    public void StartMoveFigure(Vector3 endPosition)
    {
        isMoving = true;
        this.endPosition = endPosition;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.Translate((endPosition - gameObject.transform.position) * Time.fixedDeltaTime);
        }
    }
}
