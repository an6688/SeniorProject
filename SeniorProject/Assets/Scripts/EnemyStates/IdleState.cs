using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class IdleState : IEnemyState
{
    [SerializeField] public Enemy enemy;

    private float idleTimer;

    private float idleDuration = 5;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("i am idling!");

        Idle();

        if (enemy.Target != null)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {
        Debug.Log("exit to be implemented");
    }

    public void OnTriggerEnter(Collider2D other)
    {
        Debug.Log("ontriggerenter to be implemented!");
    }

    public virtual void Idle()
    {
        if (enemy != null) enemy.MyAnimator.SetFloat("speed", 0);

        idleTimer += Time.deltaTime; // delta time represents the time passed since the last time a frame was rendered 

        if (idleTimer >= idleDuration)
        {
            if (enemy != null) enemy.ChangeState(new PatrolState());
        }
    }
}
