using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    public EnemyFindPlayer findPlayerState = new EnemyFindPlayer();
    public EnemyChase chaseState = new EnemyChase();
    public EnemyAttack attackState = new EnemyAttack();
    public EnemyState currentState;

    public float startChaseDistance = 30f;
    public float enemyPounceRange = 10f;

    [HideInInspector] public NavMeshAgent agent;

    [HideInInspector] public Transform playerTransform;

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
