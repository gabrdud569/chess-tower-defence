using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private OpponentConfig config;

    private List<PathElement> path;
    private int currentLife;
    private int currentSpeed;
    private bool isAlive = false;

    private void FixedUpdate()
    {
        if (isAlive)
        {
            transform.Translate(Vector3.forward * Time.fixedDeltaTime * currentSpeed);
        }
    }

    public void Initialize(List<PathElement> path, OpponentConfig config)
    {
        this.path = path;
        this.config = config;
        currentLife = config.maxLife;
        currentSpeed = config.speed;
        isAlive = true;
        this.enabled = true;
        StartMovement();
    }

    public void StartMovement()
    {
        animator.SetBool("Walk", true);
        animator.speed = currentSpeed;
        MoveToNextPoint(0);
    }

    public void OnPointInPathEntered(PathElement pathElement)
    {
        MoveToNextPoint(path.IndexOf(pathElement)+1);
    }

    private void MoveToNextPoint(int pointIndex)
    {
        if(path.Count > pointIndex)
        {
            transform.LookAt(path[pointIndex].GetTransform().position, Vector3.up);
        }
        else
        {
            currentLife = 0;
            RefreshMonsterState();
        }
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
            //animator.SetBool("Dead", true);
            isAlive = false;
            Destroy(this.gameObject);
        }
    }
}