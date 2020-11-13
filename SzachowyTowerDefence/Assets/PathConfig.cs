using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PathConfig", menuName = "PathConfig")]
public class PathConfig : ScriptableObject
{
    [SerializeField] public List<string> path;
}
