﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Execute();
    void Enter(Enemy enemy);
    void Exit();
    void OnTrigger(Collider2D other); 
}