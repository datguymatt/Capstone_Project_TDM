using System.Collections;
using UnityEngine;

public class AttackTransitionState : EnemyState
{
    private float distance;
    private bool canJumpAttack = true;
    private float baseSpeed;

    public override string GetClass()
    {
        var s = "AttackTransitionState";
        return s;
    }

    public override void OnStateEnter(EnemyStateManager manager)
    {
        //Debug.Log("Transitioning Attacks");
        baseSpeed = manager.agent.speed;
        manager.agent.speed = baseSpeed * manager.catchUpSpeed;

    }

    public override void OnStateExit(EnemyStateManager manager)
    {
        if (!canJumpAttack)
        {
            manager.StartCoroutine(JumpAttackCooldown(manager));
        }
        manager.agent.speed = baseSpeed;
    }

    public override void OnStateUpdate(EnemyStateManager manager)
    {
        distance = Vector3.Distance(manager.transform.position, manager.playerTransform.position);
        if (distance <= manager.jumpAttackRange)
        {
            if (distance <= manager.meleeAttackRange)
            {
                manager.currentAttack = EnemyStateManager.Attacks.meleeAttack;
                manager.ChangeState(manager.attackState);
                return;
            }
            else if (canJumpAttack)
            {
                manager.currentAttack = EnemyStateManager.Attacks.jumpAttack;
                canJumpAttack = false;
                manager.ChangeState(manager.attackState);
                return;
            }
        }
        else
        {
            EnemyStateTracker.Instance.enemyLeftAttack.Invoke();
            manager.ChangeState(manager.chaseState);
        }
        manager.LookAtPlayer(manager, manager.playerTransform.position);
        manager.agent.destination = manager.playerTransform.position;
    }

    private IEnumerator JumpAttackCooldown(EnemyStateManager manager)
    {
        yield return new WaitForSeconds(manager.jumpAttackCooldown);
        canJumpAttack = true;
        yield return null;
    }
}
