using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Path config - specifies tiles that are on path
/// </summary>
[CreateAssetMenu(fileName = "PathConfig", menuName = "PathConfig")]
public class PathConfig : ScriptableObject
{
    [SerializeField] public List<string> path;
}
