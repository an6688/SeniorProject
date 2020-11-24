using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private Enemy enemy;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {

        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new MeleeState());
        }

        else if (enemy.Target != null)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        Debug.Log("Exit in RangedSTate to be implemented!");
    }

    public void OnTriggerEnter(Collider2D other)
    {
        Debug.Log("OnTriggerEnter in RangedSTate to be implemented!");
    }
}
