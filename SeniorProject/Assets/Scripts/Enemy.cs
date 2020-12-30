﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : Character
{
    private IEnemyState currentState;

    public GameObject Target { get; set; }

    [SerializeField] private float meleeRange;

    [SerializeField] private float throwRange;

    /// <summary>
    /// Indicates if the enemy is in melee range
    /// </summary>

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //Makes the RemoveTarget function listen to the player's Dead event
        Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
        ChangeState(new IdleState());
    }

    private void RemoveTarget()
    {
        //Removes the target
        Target = null;

        //Changes the state to a patrol state
        ChangeState(new PatrolState());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null & !isDead) currentState.Execute();

        LookAtTarget();

        /*if (!isDead)
        {
            currentState.Execute();
            LookAtTarget();
        }*/
    }

    public override IEnumerator TakeDamage()
    {
        health -= 5;
        if (!isDead)
        {
            MyAnimator.SetTrigger("damage");
        }
        else
        {
            MyAnimator.SetTrigger("die");
            yield return null; 
        }
    }

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }

            return false; 
        }
    }

    /// <summary>
    /// Indicates if the enemy is in throw range
    /// </summary>
    public bool InThrowRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= throwRange;

            }
            return false;
        }
    }
  
    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        // currentState?.Exit(); 

        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void Move()
    {
        if (!Attack)
        {
            MyAnimator.SetFloat("speed", 1);

            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
        }
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }

    public override bool isDead
    {
        get
        {
            return health <= 0; 
        }
    }

    public override void Death()
    {
        Destroy(gameObject);
    }
}
