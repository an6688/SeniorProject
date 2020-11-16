using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy; 

    private float patrolTimer;

    private float patrolDuration = 10; 

    public void Execute()
    {
        Debug.Log("i am patrolling!");
        Patrol();

        enemy.Move();
    }

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void OnTrigger(Collider2D other)
    {
        throw new System.NotImplementedException();
    }

    private void Patrol()
    {
        patrolTimer += Time.deltaTime; // delta time represents the time passed since the last time a frame was rendered 

        if (patrolTimer >= patrolDuration) 
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
