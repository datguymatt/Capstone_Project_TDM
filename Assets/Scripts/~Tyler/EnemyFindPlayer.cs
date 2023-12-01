using UnityEngine;

public class EnemyFindPlayer : EnemyState
{
    private float baseSpeed;

    public override string GetClass()
    {
        var s = "EnemyFindPlayer";
        return s;
    }

    public override void OnStateEnter(EnemyStateManager manager)
    {
        baseSpeed = manager.agent.speed;
        manager.agent.speed = manager.approachSpeedMultiplyer * baseSpeed;
    }

    public override void OnStateExit(EnemyStateManager manager)
    {
        manager.agent.speed = baseSpeed;
    }

    public override void OnStateUpdate(EnemyStateManager manager)
    {
        //Checks if enemy can change state to chase the player
        if (Vector3.Distance(manager.playerTransform.position, manager.transform.position) > manager.startChaseDistance)
        {
            manager.agent.destination = ApproachAroundPlayer(manager.playerTransform.position, manager);
        }
        else
        {
            Debug.Log("Enemy Started Chasing the Player");
            manager.agent.destination = manager.playerTransform.position;

            manager.ChangeState(manager.chaseState);
        }
    }
    //Calculates the position perpendicular from enemy to player at a set distance of destinationRadiusFromPlayer
    private Vector3 ApproachAroundPlayer(Vector3 player, EnemyStateManager manager)
    {
        Vector3 difference = manager.transform.position - player;
        Vector3 direction = new Vector3(difference.z, 0, -difference.x);
        direction = direction.normalized;
        Vector3 target = player + direction * manager.destinationRadiusFromPlayer;
        return target;
    }
}
