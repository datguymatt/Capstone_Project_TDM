using UnityEngine;

public class EnemyChase : EnemyState
{
    private float distance;
    private float baseSpeed;

    public override string GetClass()
    {
        var s = "EnemyChase";
        return s;
    }

    public override void OnStateEnter(EnemyStateManager manager)
    {
        baseSpeed = manager.agent.speed;
        manager.agent.speed = baseSpeed * manager.chaseSpeedMultiplier;
    }

    public override void OnStateExit(EnemyStateManager manager)
    {
        manager.agent.speed = baseSpeed;
    }

    public override void OnStateUpdate(EnemyStateManager manager)
    {
        manager.LookAtPlayer(manager, manager.playerTransform.position);
        distance = Vector3.Distance(manager.transform.position, manager.playerTransform.position);

        if (manager.startChaseDistance >= distance && distance > manager.jumpAttackRange)
        {
            manager.agent.destination = manager.playerTransform.position;
        }
        if (distance <= manager.jumpAttackRange)
        {
            //Check how many vampires are in attack state already if there are too many start circling player and wait to enter attack state otherwise enter attackstate
            if (EnemyStateTracker.Instance.EnemiesInState(manager.attackState.GetClass(), manager.transitionState.GetClass()) >= EnemyStateTracker.Instance.GetMaxAttackingEnemies())
            {
                manager.ChangeState(manager.circleState);
            }
            else
            {
                manager.ChangeState(manager.transitionState);
            }

        }
        if (distance > manager.startChaseDistance)
        {
            manager.ChangeState(manager.findPlayerState);
        }
    }
}
