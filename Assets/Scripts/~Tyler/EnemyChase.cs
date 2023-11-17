using UnityEngine;

public class EnemyChase : EnemyState
{
    public override void OnStateEnter(EnemyStateManager manager)
    {

    }

    public override void OnStateExit(EnemyStateManager manager)
    {
        Debug.Log("Exiting Chase State");
    }

    public override void OnStateUpdate(EnemyStateManager manager)
    {
        if (manager.startChaseDistance + 2f >= Vector3.Distance(manager.transform.position, manager.playerTransform.position) && Vector3.Distance(manager.transform.position, manager.playerTransform.position) > manager.enemyPounceRange)
        {
            manager.agent.destination = manager.playerTransform.position;
        }
        if (Vector3.Distance(manager.transform.position, manager.playerTransform.position) <= manager.enemyPounceRange)
        {
            //Non implemented feature* 
            //Check how many vampires are in attack state already if there are too many start circling player and wait to enter attack state otherwise enter attackstate 
            manager.ChangeState(manager.attackState);
        }
    }
}
