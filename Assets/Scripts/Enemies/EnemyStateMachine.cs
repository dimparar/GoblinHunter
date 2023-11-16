using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using static EnemyAnimationFSM;

public class EnemyStateMachine : StateManager<EnemyStateMachine.EnemyState>
{

    public enum EnemyState
    {
        ATTACK, PATROL, CHASE, DEAD
    }

    private SpriteBillboard billboard;
    public EnemyController controller;
    private bool _isAttacking = false;

    public bool _isIdling = false;
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
        States.Add(EnemyState.DEAD, new DeathEnemyState(this));
    }

    private void Awake()
    {
        Player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        billboard = GetComponentInChildren<SpriteBillboard>();
        controller = GetComponent<EnemyController>();
        EventManager.EnemyDeathEvent += DropDead;
        EventManager.EnemyAttackEvent += StartAttack;
        agent.updateRotation = false;
        Agent.isStopped = true;

        InitStates();
        CurrentState = States[EnemyState.PATROL];
    }

    private void LateUpdate()
    {
        if (CurrentState.StateKey != EnemyState.DEAD)
        {

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

    public void DropDead(Hittable enemy)
    {
        if (gameObject.GetComponent<Hittable>() == enemy)
        {
            TransitionToState(EnemyState.DEAD);
            TerminateFSM = true;
            billboard.rotateYAxis = true;
            GetComponent<Collider>().enabled = false;
            // After he dies, just drop
            agent.speed = 0;
        }
    }

    /// <summary>
    /// Starts Attack function for the enemy from the AttackEnemyState
    /// </summary>
    public void StartAttack()
    {
        StartCoroutine(AttackAndWait());
    }
    /// <summary>
    /// Starts Idle&Wait Coroutine
    /// </summary>
    public void StartIdle()
    {
        StartCoroutine(IdleAndWait());
    }

    /// <summary>
    /// Enables the attack animation event and attacks the player
    /// </summary>
    /// <returns></returns>
    public IEnumerator AttackAndWait()
    {
        isAttacking = true;

        // Perform the attack action here (e.g. deal damage, etc.)
        Debug.Log("Enemy is attacking!");

/*        EventManager.EnemyAttackPerform();
*/
        // Wait for the specified attack interval as long as the animation is and a little more
        yield return new WaitForSeconds(2f);

        isAttacking = false;
    }

    public IEnumerator IdleAndWait()
    {
        Agent.speed = 0;
        _isIdling = true;
        // Wait for the specified idle interval
        yield return new WaitForSeconds(5f);

        Agent.isStopped = true;
    }
}
