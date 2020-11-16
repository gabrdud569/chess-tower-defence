using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathProvider : MonoBehaviour
{
    [SerializeField] private List<PathElement> startPathElements;
    [SerializeField] private List<PathElement> endPathElements;

    public List<PathElement> StartPathElements => startPathElements;
    public List<PathElement> EndPathElements => endPathElements;
}
