using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : MonoBehaviour
{
    private Transform target_Player;
    private NavMeshAgent agent;

    private void Start()
    {
        target_Player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Follow(target_Player);
    }

    private void Follow(Transform destination)
    {
        agent.SetDestination(destination.position);
    }
}
