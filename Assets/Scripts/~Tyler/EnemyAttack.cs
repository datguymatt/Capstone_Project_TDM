using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : EnemyState
{
    private float distance;
    private bool isAttacking;
    private bool canJumpAttack = true;
    public override void OnStateEnter(EnemyStateManager manager)
    {
        Debug.Log("Starting Attack");
        manager.StartCoroutine(this.JumpAttack(manager, manager.playerTransform));
    }

    public override void OnStateExit(EnemyStateManager manager)
    {
        Debug.Log("Exiting Attack State");
    }

    public override void OnStateUpdate(EnemyStateManager manager)
    {
        distance = Vector3.Distance(manager.transform.position, manager.playerTransform.position);
        if (distance > manager.enemyPounceRange && !isAttacking)
        {
            manager.StopAllCoroutines();
            manager.ChangeState(manager.chaseState);
        }

        if (!canJumpAttack && !isAttacking && distance > manager.enemyPounceRange * 0.5f)
        {
            //manager.StopAllCoroutines();
            //manager.ChangeState(manager.circleState)
        }
    }

    private IEnumerator JumpAttack(EnemyStateManager manager, Transform player)
    {
        isAttacking = true;
        manager.agent.enabled = false;

        Vector3 startingPosition = manager.transform.position;

        //SetTrigger for jump animation to start
        //Channeling attack
        Debug.Log("Enemy About To Jump");
        yield return new WaitForSeconds(0.25f);

        Vector3 target = new Vector3(player.position.x, startingPosition.y, player.position.z);
        for (float time = 0; time < 1; time += Time.deltaTime * manager.jumpSpeed)
        {
            manager.transform.position = Vector3.Lerp(startingPosition, target - Vector3.forward, time) + Vector3.up * manager.jumpCurve.Evaluate(time);
            manager.transform.rotation = Quaternion.Slerp(manager.transform.rotation, Quaternion.LookRotation(new Vector3(player.position.x, manager.transform.position.y, player.position.z) - manager.transform.position), time);
            yield return null;
        }
        manager.StartCoroutine(LaunchAttack(manager.attackHitboxes[1], manager.attackDamage, isAttacking));
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Jump Attack Ended");
        //SetTrigger for attack animation to end

        manager.agent.enabled = true;
        if (NavMesh.SamplePosition(manager.transform.position, out NavMeshHit hit, 1f, manager.agent.areaMask))
        {
            manager.agent.Warp(hit.position);
        }

        manager.agent.destination = player.position;
        isAttacking = false;

        if (distance > manager.meleeAttackRange)
        {
            manager.StartCoroutine(MeleeAttack(manager, player, 2f, manager.meleeAttackRange));
        }

        //Jump Attack Cooldown
        canJumpAttack = false;
        yield return new WaitForSeconds(5f);
        canJumpAttack = true;
    }


    private IEnumerator MeleeAttack(EnemyStateManager manager, Transform player, float cooldown, float attackRange)
    {
        bool meleeRange = true;
        //melee attack
        while (meleeRange)
        {
            manager.agent.updateRotation = true;
            manager.agent.isStopped = true;
            manager.attackHitboxes[0].enabled = true;
            isAttacking = true;
            manager.StartCoroutine(LaunchAttack(manager.attackHitboxes[0], manager.attackDamage, isAttacking));
            yield return new WaitForSeconds(0.5f);
            isAttacking = false;
            manager.agent.isStopped = false;
            manager.attackHitboxes[0].enabled = false;
            manager.agent.destination = player.position;
            yield return new WaitForSeconds(cooldown);
            if (distance > attackRange) { meleeRange = false; }
        }
        if (distance > attackRange && distance < manager.enemyPounceRange && canJumpAttack)
        {
            manager.StartCoroutine(JumpAttack(manager, manager.playerTransform));
        }
        else
        {
            Debug.Log(manager.agent.enabled);
            manager.agent.destination = player.position;
        }
        yield return null;
    }

    private IEnumerator LaunchAttack(Collider col, float damage, bool attacking)
    {
        col.enabled = true;
        while (attacking)
        {
            var cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation);
            foreach (Collider c in cols)
            {
                if (c.CompareTag("Player"))
                {
                    IDamageable damageable = c.GetComponent<IDamageable>();
                    damageable.GetDamage(damage);
                    col.enabled = false;
                    yield break;
                }
            }
            yield return null;
        }
        Debug.Log("Attack ended");
        col.enabled = false;
        yield return null;
    }
}
