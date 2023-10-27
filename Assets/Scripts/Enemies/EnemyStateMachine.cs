using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
{
    public enum EnemyState
    {
        ATTACK, PATROL, CHASE
    }

    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    public GameObject Player;
    public Path path;

    void InitStates()
    {
        States.Add(EnemyState.ATTACK, new AttackEnemyState());
        States.Add(EnemyState.PATROL, new PatrolState(this));
        States.Add(EnemyState.CHASE, new ChaseEnemyState(this));
    }

    private void Awake()
    {
        Player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;

        InitStates();
        CurrentState = States[EnemyState.PATROL];
    }

    private void LateUpdate()
    {
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
        //transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }

    private void OnDrawGizmos()
    {
        // Draw a wireframe sphere to represent the detection radius in the Unity editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(2.31365466f, 1.338305f, -38.7388077f), 10f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 7f);
    }

    public bool PlayerDetected(float detection_radius)
    {
        // Perform a sphere cast to check for objects within the detection radius
        Collider[] hits = Physics.OverlapSphere(transform.position, detection_radius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                // An object with the playerTag is inside the detection radius
                return true;
            }
        }
        return false;
    }
}
