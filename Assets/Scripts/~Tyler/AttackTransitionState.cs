using System.Collections;
using UnityEngine;

public class AttackTransitionState : EnemyState
{
    private float distance;
    private bool canJumpAttack = true;
    public override void OnStateEnter(EnemyStateManager manager)
    {
        manager.agent.enabled = true;

        Debug.Log("Transitioning Attacks");

        if (!canJumpAttack)
        {
            manager.StartCoroutine(JumpAttackCooldown(manager));
        }
    }

    public override void OnStateExit(EnemyStateManager manager)
    {

    }

    public override void OnStateUpdate(EnemyStateManager manager)
    {
        manager.agent.destination = manager.playerTransform.position;
        distance = Vector3.Distance(manager.transform.position, manager.playerTransform.position);
        if (distance <= manager.jumpAttackRange)
        {
            if (distance <= manager.meleeAttackRange)
            {
                manager.currentAttack = EnemyStateManager.Attacks.meleeAttack;
                manager.ChangeState(manager.attackState);
                return;
            }
            if (canJumpAttack && distance > manager.meleeAttackRange)
            {
                manager.currentAttack = EnemyStateManager.Attacks.jumpAttack;
                canJumpAttack = false;
                manager.ChangeState(manager.attackState);
                return;
            }
            else
            {
                manager.ChangeState(manager.circleState);
                return;
            }
        }
    }

    private IEnumerator JumpAttackCooldown(EnemyStateManager manager)
    {
        yield return new WaitForSeconds(manager.jumpAttackCooldown);
        canJumpAttack = true;
        yield return null;
    }
}
