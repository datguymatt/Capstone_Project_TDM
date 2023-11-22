using UnityEngine;

public class EnemyChase : EnemyState
{
    private float distance;
    public override void OnStateEnter(EnemyStateManager manager)
    {

    }

    public override void OnStateExit(EnemyStateManager manager)
    {
        Debug.Log("Exiting Chase State");
    }

    public override void OnStateUpdate(EnemyStateManager manager)
    {
        distance = Vector3.Distance(manager.transform.position, manager.playerTransform.position);

        if (manager.startChaseDistance >= distance && distance > manager.jumpAttackRange)
        {
            manager.agent.destination = manager.playerTransform.position;
        }
        if (distance <= manager.jumpAttackRange)
        {
            //Non implemented feature* 
            //Check how many vampires are in attack state already if there are too many start circling player and wait to enter attack state otherwise enter attackstate 
            manager.ChangeState(manager.circleState);
        }
        if (distance > manager.startChaseDistance)
        {
            manager.ChangeState(manager.findPlayerState);
        }
    }
}
