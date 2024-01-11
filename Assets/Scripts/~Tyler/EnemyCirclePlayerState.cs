using System.Collections;
using UnityEngine;

public class EnemyCirclePlayerState : EnemyState
{
    private float distance;
    private float baseSpeed;
    private bool canAttack = false;


    public override string GetClass()
    {
        var s = "EnemyCirclePlayerState";
        return s;
    }

    public override void OnStateEnter(EnemyStateManager manager)
    {
        baseSpeed = manager.agent.speed;
        manager.agent.speed = baseSpeed * manager.circleSpeedMultiplyer;
        EnemyStateTracker.Instance.enemyLeftAttack.AddListener(TryAttack);
        manager.StartCoroutine(TryAttackTimer(manager.tryAttackingFrequency));
    }

    public override void OnStateExit(EnemyStateManager manager)
    {
        manager.animator.SetBool("IsStrafing", false);
        manager.agent.speed = baseSpeed;
        canAttack = false;
        EnemyStateTracker.Instance.enemyLeftAttack.RemoveListener(TryAttack);
    }

    public override void OnStateUpdate(EnemyStateManager manager)
    {
        distance = Vector3.Distance(manager.transform.position, manager.playerTransform.position);
        manager.LookAtPlayer(manager, manager.playerTransform.position);

        if (canAttack)
        {
            if (EnemyStateTracker.Instance.EnemiesInState(manager.attackState.GetClass(), manager.transitionState.GetClass()) <= EnemyStateTracker.Instance.GetMaxAttackingEnemies())
            {
                //Debug.Log("Another enemy stopped attacking");                
                manager.ChangeState(manager.transitionState);
            }
            else
            {
                canAttack = false;
                manager.StartCoroutine(TryAttackTimer(manager.tryAttackingFrequency));
            }
        }

        /*if (distance <= manager.jumpAttackRange - 3f)
        {
            manager.animator.SetBool("IsStrafing", false);
            manager.agent.Move(-manager.transform.forward * manager.catchUpSpeed * Time.deltaTime);
        }
        else if (distance >= manager.jumpAttackRange)
        {
            manager.animator.SetBool("IsStrafing", false);
            manager.agent.Move(manager.transform.forward * manager.catchUpSpeed * Time.deltaTime);
        }*/

        StrafeLeft(manager, manager.playerTransform.position);

        if (distance > manager.jumpAttackRange)
        {
            manager.ChangeState(manager.chaseState);
        }
    }
    private void StrafeLeft(EnemyStateManager manager, Vector3 player)
    {
        manager.animator.SetBool("IsStrafing", true);
        var offset = manager.transform.position - player;
        var dir = Vector3.Cross(offset, Vector3.up);
        manager.agent.SetDestination(manager.transform.position + dir);

    }

    private void StrafeRight(EnemyStateManager manager, Vector3 player)
    {
        var offset = player - manager.transform.position;
        var dir = Vector3.Cross(offset, Vector3.up);
        manager.agent.SetDestination(manager.transform.position + dir);
    }

    private void TryAttack()
    {
        //Debug.Log("TryAttacking");
        canAttack = true;
    }

    private IEnumerator TryAttackTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        canAttack = true;
        yield return null;
    }
}

