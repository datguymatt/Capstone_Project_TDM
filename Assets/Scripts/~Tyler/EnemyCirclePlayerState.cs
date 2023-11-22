using UnityEngine;

public class EnemyCirclePlayerState : EnemyState
{
    private float distance;
    private float baseSpeed;
    public override void OnStateEnter(EnemyStateManager manager)
    {
        baseSpeed = manager.agent.speed;
        manager.agent.updateRotation = false;
        //manager.agent.speed = baseSpeed * manager.circleSpeedMultiplyer;
    }

    public override void OnStateExit(EnemyStateManager manager)
    {
        manager.agent.speed = baseSpeed;
    }

    public override void OnStateUpdate(EnemyStateManager manager)
    {
        distance = Vector3.Distance(manager.transform.position, manager.playerTransform.position);

        //ApproachAroundPlayer(manager.playerTransform.position, manager);
        StrafeLeft(manager, manager.playerTransform.position);
        //if (distance > manager.destinationRadiusFromPlayer)
        //{
        //    manager.ChangeState(manager.chaseState);
        //}
    }

    private void StrafeLeft(EnemyStateManager manager, Vector3 player)
    {
        var offset = manager.transform.position - player;
        var dir = Vector3.Cross(offset, Vector3.up);
        manager.agent.SetDestination(manager.transform.position + dir);
        var lookPos = player - manager.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        manager.transform.rotation = Quaternion.Slerp(manager.transform.rotation, rotation, Time.deltaTime * 2f);
    }
}

