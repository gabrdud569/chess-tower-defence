﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathElement : MonoBehaviour
{
    public Transform GetTransform()
    {
        return this.transform;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<OpponentController>() != null)
        {
            other.GetComponent<OpponentController>().OnPointInPathEntered(this);
        }
    }
}
