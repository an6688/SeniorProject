using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class IdleState : IEnemyState
{
    public Enemy enemy;

    private float idleTimer;

    private float idleDuration = 5;

    public void Execute()
    {
        Debug.Log("i am idling!");

        Idle();
    }

    public void Enter(Enemy enemy)
    {
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void OnTrigger(Collider2D other)
    {
        throw new System.NotImplementedException();
    }

    private void Idle()
    {
        enemy.MyAnimator.SetFloat("speed", 1);
        
        idleTimer += Time.deltaTime; // delta time represents the time passed since the last time a frame was rendered 

        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new PatrolState());   
        }
    }
}
