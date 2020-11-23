using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private OpponentConfig config;

    public event Action<int> OnDamageDealed = delegate { };
    public event Action<int> OnDead = delegate { };

    private List<PathElement> path;
    private PathElement currentPathElement;
    private int currentLife;
    private float currentSpeed;
    private int damage;
    private bool isAlive = false;
    private bool destructible = false;

    private void FixedUpdate()
    {
        if (isAlive)
        {
            transform.Translate(Vector3.forward * Time.fixedDeltaTime * currentSpeed);
        }
    }

    public void Initialize(List<PathElement> path, OpponentConfig config, int multiplier)
    {
        this.path = path;
        this.config = config;
        
        currentLife = config.maxLife * Mathf.Max(1, Convert.ToInt32(multiplier / 10f));
        currentSpeed = config.speed*Mathf.Min(Mathf.Max(1, Convert.ToInt32(multiplier / 10f)),8f);
        damage = (int)config.damage* Mathf.Max(1, Convert.ToInt32(multiplier/10f));
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
        if(currentPathElement != null)
        {
            currentPathElement.LeaveFromThisPoint(this);
        }

        currentPathElement = pathElement;
        MoveToNextPoint(path.IndexOf(pathElement)+1);
    }

    public void DealDamage(int damage)
    {
        if(destructible)
        {
            currentLife -= damage;
            RefreshMonsterState();
        }
    }

    public void OnEndPointAchieved()
    {
        destructible = false;
        OnDamageDealed(damage); 
    }

    public void OnStartMoveOnBoard()
    {
        destructible = true;
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
            RefreshMonsterState(false);
        }
    }

    private void RefreshMonsterState(bool withCallback = true)
    {
        if(currentLife <= 0)
        {
            isAlive = false;

            if (currentPathElement != null)
            {
                currentPathElement.LeaveFromThisPoint(this);
            }

            if (withCallback)
            {
                OnDead(config.reward);
            }

            Destroy(this.gameObject);
        }
    }
}