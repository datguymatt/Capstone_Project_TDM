using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyState
{
    public override void OnStateEnter(EnemyStateManager manager)
    {
        Debug.Log("Starting Attack");
    }

    public override void OnStateExit(EnemyStateManager manager)
    {
        Debug.Log("Exiting Attack State");
    }

    public override void OnStateUpdate(EnemyStateManager manager)
    {
        
    }

    private IEnumerator JumpAttack(EnemyStateManager manager, Transform player)
    {
        manager.agent.enabled = false;
        yield return null;
    }
}
