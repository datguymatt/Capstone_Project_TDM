using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : EnemyState
{
    private float distance;
    private bool isAttacking;
    public override void OnStateEnter(EnemyStateManager manager)
    {
        //Debug.Log("Starting Attack");
        if (manager.currentAttack == EnemyStateManager.Attacks.jumpAttack)
        {
            manager.StartCoroutine(this.JumpAttack(manager, manager.playerTransform));
        }
        if (manager.currentAttack == EnemyStateManager.Attacks.meleeAttack)
        {
            manager.StartCoroutine(this.MeleeAttack(manager, manager.playerTransform, 1f));
        }
    }

    public override void OnStateExit(EnemyStateManager manager)
    {

    }

    public override void OnStateUpdate(EnemyStateManager manager)
    {
        distance = Vector3.Distance(manager.transform.position, manager.playerTransform.position);

        if (!isAttacking)
        {
            manager.LookAtPlayer(manager, manager.playerTransform.position);
            manager.agent.destination = manager.playerTransform.position;
        }
    }

    private IEnumerator JumpAttack(EnemyStateManager manager, Transform player)
    {
        isAttacking = true;
        manager.agent.enabled = false;

        Vector3 startingPosition = manager.transform.position;

        //SetTrigger for jump animation to start
        manager.animator.SetTrigger("JumpAttack");
        //Channeling attack
        //Debug.Log("Enemy About To Jump");
        //yield return new WaitForSeconds(0.5f);

        Vector3 target = new Vector3(player.position.x, startingPosition.y, player.position.z);
        for (float time = 0; time < 1; time += Time.deltaTime * manager.jumpAttackSpeed)
        {
            if (time < 0.65f)
            {
                target = new Vector3(player.position.x, startingPosition.y, player.position.z);
            }
            manager.transform.position = Vector3.Lerp(startingPosition, target, time) + Vector3.up * manager.jumpCurve.Evaluate(time);
            manager.transform.rotation = Quaternion.Slerp(manager.transform.rotation, Quaternion.LookRotation(new Vector3(target.x, manager.transform.position.y, target.z) - manager.transform.position), time);
            yield return null;
        }
        //play sfx
        int randomFileNumber = Random.Range(1, 4);
        GameObject.FindAnyObjectByType<AudioManager>().PlaySFXAudio("enemy-attack-" + randomFileNumber.ToString());
        //
        manager.StartCoroutine(LaunchAttack(manager.attackHitboxes[1], manager.attackDamage, isAttacking));
        yield return new WaitForSeconds(0.15f);
        //Debug.Log("Jump Attack Ended");
        //SetTrigger for attack animation to end

        manager.agent.enabled = true;
        if (NavMesh.SamplePosition(manager.transform.position, out NavMeshHit hit, 1f, manager.agent.areaMask))
        {
            manager.agent.Warp(hit.position);
        }

        isAttacking = false;
        yield return new WaitForSeconds(0.5f);
        manager.ChangeState(manager.transitionState);
    }


    private IEnumerator MeleeAttack(EnemyStateManager manager, Transform player, float cooldown)
    {
        manager.animator.SetTrigger("Attack");
        isAttacking = true;
        manager.agent.isStopped = true;
        manager.StartCoroutine(LaunchAttack(manager.attackHitboxes[0], manager.attackDamage, isAttacking));
        yield return new WaitForSeconds(0.2f);
        manager.agent.isStopped = false;
        isAttacking = false;
        yield return new WaitForSeconds(cooldown);
        manager.ChangeState(manager.transitionState);
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
                    if (damageable != null)
                    {
                        damageable.GetDamage(damage);
                        col.enabled = false;
                        yield break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            yield return null;
        }
        //Debug.Log("Attack ended");
        col.enabled = false;
        yield return null;
    }

    public override string GetClass()
    {
        var s = "EnemyAttack";
        return s;
    }
}
