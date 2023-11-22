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

    public float destinationRadiusFromPlayer = 15f;
    public float approachSpeedMultiplyer = 0.6f;
    public float circleSpeedMultiplyer = 0.2f;

    [Header("Chase Variables")]
    public float startChaseDistance = 20f;

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
    }

    private void Update()
    {
        currentState.OnStateUpdate(this);
    }

    public void ChangeState(EnemyState newState)
    {
        currentState.OnStateExit(this);
        currentState = newState;
        //Enemy State Tracker event call to update this enemies current state to newState
        currentState.OnStateEnter(this);
    }
}
