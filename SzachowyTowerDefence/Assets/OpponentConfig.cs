using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OpponentConfig", menuName = "OpponentConfig")]
public class OpponentConfig : ScriptableObject
{
    [SerializeField] public int maxLife;
    [SerializeField] public int speed;
    [SerializeField] public int damage;
}
