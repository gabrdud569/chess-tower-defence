using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Opponent Controller - conrtols enemies
/// </summary>
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

    /// <summary>
    /// Moves alive enemies forward
    /// </summary>
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
        currentSpeed = config.speed * Mathf.Min(Mathf.Max(1, Convert.ToInt32(multiplier / 10f)), 5f);
        currentSpeed = Mathf.Min(5f, currentSpeed);
        damage = (int)config.damage * Mathf.Max(1, Convert.ToInt32(multiplier / 10f));
        isAlive = true;
        this.enabled = true;
        StartMovement();
    }

    /// <summary>
    /// Initializes enemy movement
    /// </summary>
    public void StartMovement()
    {
        animator.SetBool("Walk", true);
        animator.speed = currentSpeed;
        MoveToNextPoint(0);
    }

    /// <summary>
    /// Invoked after enemy enters new map tile
    /// </summary>
    /// <param name="pathElement">Tile on map</param>
    public void OnPointInPathEntered(PathElement pathElement)
    {
        if (currentPathElement != null)
        {
            currentPathElement.LeaveFromThisPoint(this);
        }

        currentPathElement = pathElement;
        MoveToNextPoint(path.IndexOf(pathElement) + 1);
    }

    /// <summary>
    /// Invoked after bullet hits enemy
    /// </summary>
    /// <param name="damage"></param>
    public void DealDamage(int damage)
    {
        if (destructible)
        {
            currentLife -= damage;
            RefreshMonsterState();
        }
    }

    /// <summary>
    /// Invoked after enemy reaches finish line
    /// </summary>
    public void OnEndPointAchieved()
    {
        destructible = false;
        OnDamageDealed(damage);
    }

    /// <summary>
    /// Invoed after enemy starts moving on board
    /// </summary>
    public void OnStartMoveOnBoard()
    {
        destructible = true;
    }

    /// <summary>
    /// Changes point where enemy looks at given point, depends on path on map
    /// </summary>
    /// <param name="pointIndex">Tile on map index</param>
    private void MoveToNextPoint(int pointIndex)
    {
        if (path.Count > pointIndex)
        {
            transform.LookAt(path[pointIndex].GetTransform().position, Vector3.up);
        }
        else
        {
            currentLife = 0;
            RefreshMonsterState(false);
        }
    }

    /// <summary>
    /// Refreshes monster state and checks wheter it's dead
    /// </summary>
    /// <param name="withCallback">Decides ehether callback should be invoked</param>
    private void RefreshMonsterState(bool withCallback = true)
    {
        if (currentLife <= 0)
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