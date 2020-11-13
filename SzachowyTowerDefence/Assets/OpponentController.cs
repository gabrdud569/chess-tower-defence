using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private OpponentConfig config;

    private int currentLife;
    private int currentSpeed;
    private bool isAlive = true;

    private void OnEnable()
    {
        Initialize();
        StartMovement();
    }

    private void Update()
    {
        if (isAlive)
        {
            SetDamage(1);
        }
        else
        {
            //animator
        }
    }

    public void Initialize()
    {
        currentLife = config.maxLife;
        currentSpeed = config.speed;
    }

    public void SetMovementPath(List<Transform> pathPoints)
    {
        
    }

    public void StartMovement()
    {
        animator.SetBool("Walk", true);
    }

    public void SetDamage(int damage)
    {
        currentLife -= damage;
        RefreshMonsterState();
    }

    private void RefreshMonsterState()
    {
        if(currentLife <= 0)
        {
            animator.SetBool("Dead", true);
            isAlive = false;
        }
    }
}