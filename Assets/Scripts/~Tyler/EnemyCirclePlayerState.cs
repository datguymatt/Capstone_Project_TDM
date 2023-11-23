using UnityEngine;

public class EnemyCirclePlayerState : EnemyState
{
    private float distance;
    private float baseSpeed;
    public override void OnStateEnter(EnemyStateManager manager)
    {
        baseSpeed = manager.agent.speed;
        manager.agent.updateRotation = false;
        manager.agent.speed = baseSpeed * manager.circleSpeedMultiplyer;
    }

    public override void OnStateExit(EnemyStateManager manager)
    {
        manager.agent.speed = baseSpeed;
    }

    public override void OnStateUpdate(EnemyStateManager manager)
    {
        distance = Vector3.Distance(manager.transform.position, manager.playerTransform.position);
        LookAtPlayer(manager, manager.playerTransform.position);
        if (distance <= manager.jumpAttackRange)
        {
            manager.agent.Move(-manager.transform.forward * manager.catchUpSpeed * Time.deltaTime);
        }
        if (distance >= manager.destinationRadiusFromPlayer - 2f)
        {
            manager.agent.Move(manager.transform.forward * manager.catchUpSpeed * Time.deltaTime);
        }
        else if (distance > manager.jumpAttackRange)
        {
            StrafeLeft(manager, manager.playerTransform.position);
        }
        if (distance > manager.destinationRadiusFromPlayer + 2f)
        {
            manager.ChangeState(manager.chaseState);
        }


    }

    private void LookAtPlayer(EnemyStateManager manager, Vector3 player)
    {
        var lookPos = player - manager.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        manager.transform.rotation = Quaternion.Slerp(manager.transform.rotation, rotation, Time.deltaTime * 2.5f);
    }
    private void StrafeLeft(EnemyStateManager manager, Vector3 player)
    {
        var offset = manager.transform.position - player;
        var dir = Vector3.Cross(offset, Vector3.up);
        manager.agent.SetDestination(manager.transform.position + dir);

    }

    private void StrafeRight(EnemyStateManager manager, Vector3 player)
    {
        var offset = player - manager.transform.position;
        var dir = Vector3.Cross(offset, Vector3.up);
        manager.agent.SetDestination(manager.transform.position + dir);
        var lookPos = player - manager.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        manager.transform.rotation = Quaternion.Slerp(manager.transform.rotation, rotation, Time.deltaTime * 2f);
    }
}

