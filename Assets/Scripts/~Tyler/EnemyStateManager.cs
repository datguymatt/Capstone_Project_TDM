using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    public EnemyFindPlayer findPlayerState = new EnemyFindPlayer();
    public EnemyChase chaseState = new EnemyChase();
    public EnemyCirclePlayerState circleState = new EnemyCirclePlayerState();
    public EnemyAttack attackState = new EnemyAttack();
    public AttackTransitionState transitionState = new AttackTransitionState();
    public EnemyState currentState;

    public Animator animator;


    public float destinationRadiusFromPlayer = 10f;
    public float approachSpeedMultiplyer = 0.6f;
    public float circleSpeedMultiplyer = 0.2f;
    public float catchUpSpeed = 2.5f;

    [Header("Chase Variables")]
    public float startChaseDistance = 25f;
    public float chaseSpeedMultiplier = 2;

    [Header("Attack Variables")]
    public float meleeAttackRange = 1.5f;
    public float jumpAttackRange = 10f;
    public float jumpAttackCooldown = 5f;
    public AnimationCurve jumpCurve;
    public float jumpAttackSpeed = 1f;
    public float attackDamage = 1f;
    public Attacks currentAttack = Attacks.meleeAttack;
    public enum Attacks
    {
        jumpAttack,
        meleeAttack,
    }

    public Collider[] attackHitboxes;

    [HideInInspector] public NavMeshAgent agent;

    [HideInInspector] public Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = findPlayerState;
        currentState.OnStateEnter(this);
        agent.updateRotation = false;

    }

    private void Update()
    {
        currentState.OnStateUpdate(this);
        Vector3 normalizedMovement = agent.velocity;
        Vector3 forwardVector = Vector3.Project(normalizedMovement, transform.forward);
        Vector3 rightVector = Vector3.Project(normalizedMovement, transform.right);
        float forwardVelocity = forwardVector.magnitude * Vector3.Dot(forwardVector, transform.forward);
        float rightVelocity = rightVector.magnitude * Vector3.Dot(rightVector, transform.right);

        animator.SetFloat("RightMovement", rightVelocity);
        animator.SetFloat("ForwardMovement", Mathf.InverseLerp(-1f, 2f, forwardVelocity));
        animator.SetFloat("ForwardVelocity", Mathf.InverseLerp(-1f, 5f, forwardVector.magnitude));
        animator.SetFloat("RightVelocity", Mathf.InverseLerp(-1f, 1f, rightVelocity));
    }

    public void ChangeState(EnemyState newState)
    {
        //Debug.Log($"Exiting{currentState} and Entering {newState}");
        currentState.OnStateExit(this);
        currentState = newState;
        EnemyStateTracker.Instance.UpdateEnemyState(this.gameObject, newState.GetClass());
        //Enemy State Tracker event call to update this enemies current state to newState
        currentState.OnStateEnter(this);
    }

    public void LookAtPlayer(EnemyStateManager manager, Vector3 player)
    {
        var lookPos = player - manager.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        manager.transform.rotation = Quaternion.Slerp(manager.transform.rotation, rotation, Time.deltaTime * 3f);
    }
}
