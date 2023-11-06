using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
{
    public enum EnemyState
    {
        ATTACK, PATROL, CHASE, DEAD
    }

    private SpriteBillboard billboard;
    private EnemyController controller;
    private bool _isAttacking = false;
    public bool isAttacking
    {
        get => _isAttacking;
        set => _isAttacking = value;
    }
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    public GameObject Player;
    public Path path;

    void InitStates()
    {
        States.Add(EnemyState.ATTACK, new AttackEnemyState(this));
        States.Add(EnemyState.PATROL, new PatrolEnemyState(this));
        States.Add(EnemyState.CHASE, new ChaseEnemyState(this));
        States.Add(EnemyState.DEAD, new DeathEnemyState());
    }

    private void Awake()
    {
        Player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        billboard = GetComponentInChildren<SpriteBillboard>();
        controller = GetComponent<EnemyController>();
        EventManager.EnemyDeathEvent += DropDead;

        agent.updateRotation = false;

        InitStates();
        CurrentState = States[EnemyState.PATROL];
    }

    private void LateUpdate()
    {
        if (CurrentState.StateKey != EnemyState.DEAD)
        {
            if (controller.Health == 0)
            {
                EventManager.EnemyDeath();
            }

            if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
            }
            //transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a wireframe sphere to represent the detection radius in the Unity editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(PatrolEnemyState.nextWaypoint, 1f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 7f);
    }

    public bool PlayerDetected(float detection_radius)
    {
        // Perform a sphere cast to check for objects within the detection radius
        Collider[] hits = Physics.OverlapSphere(transform.position, detection_radius);

        foreach (Collider hit in hits)
        {
            Vector3 playerPosition = hit.transform.position;

            // Check if the player's position is on the NavMesh: 2f is enough distance but we have a problem when the player is out of bounds
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(playerPosition, out navHit, 2f, NavMesh.AllAreas) && hit.CompareTag("Player"))
            {
                // The player is on the NavMesh, you can proceed with your logic here
                // For example, trigger an event, follow the player, etc.
                return true;
            }
        }
        return false;
    }

    public void DropDead()
    {
        TransitionToState(EnemyState.DEAD);
        TerminateFSM = true;
        billboard.rotateYAxis = true;
        // After he dies, just drop
        agent.speed = 0;
    }

    public IEnumerator AttackAndWait()
    {
        isAttacking = true;
        // Perform the attack action here (e.g., play animation, deal damage, etc.)
        Debug.Log("Enemy is attacking!");

        // Wait for the specified attack interval
        yield return new WaitForSeconds(4f);

        isAttacking = false;
    }
}
