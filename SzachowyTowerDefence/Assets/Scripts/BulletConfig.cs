
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletConfig", menuName = "BulletConfig")]
public class BulletConfig : ScriptableObject
{
    [SerializeField] public float speed;
}