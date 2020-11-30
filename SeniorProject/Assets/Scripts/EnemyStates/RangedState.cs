﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private Enemy enemy;

    private float throwTimer;

    private float throwCoolDown = 3;

    private bool canThrow; 
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        ThrowKnife();
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

    private void ThrowKnife()
    {
        throwTimer += Time.deltaTime;

        if (throwTimer >= throwCoolDown)
        {
            canThrow = true;
            throwTimer = 0; 
        }

        if (canThrow)
        {
            canThrow = false; 
            enemy.MyAnimator.SetTrigger("throw");
        }
    }
}