using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public abstract void OnStateEnter(EnemyStateManager manager);
    public abstract void OnStateUpdate(EnemyStateManager manager);
    public abstract void OnStateExit(EnemyStateManager manager);
}
