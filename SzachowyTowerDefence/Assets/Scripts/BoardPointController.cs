using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPointController : PathElement
{
    [SerializeField] private string name;

    public string Name => name;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
