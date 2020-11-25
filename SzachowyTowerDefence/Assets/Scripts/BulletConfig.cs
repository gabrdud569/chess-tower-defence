
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bullet config - specifies bullet speed
/// </summary>
[CreateAssetMenu(fileName = "BulletConfig", menuName = "BulletConfig")]
public class BulletConfig : ScriptableObject
{
    [SerializeField] public float speed;
}