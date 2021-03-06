﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Opponent config - specifies enemy life, speed, damage, reward
/// </summary>
[CreateAssetMenu(fileName = "OpponentConfig", menuName = "OpponentConfig")]
public class OpponentConfig : ScriptableObject
{
    [SerializeField] public int maxLife;
    [SerializeField] public int speed;
    [SerializeField] public int damage;
    [SerializeField] public int reward;
}
