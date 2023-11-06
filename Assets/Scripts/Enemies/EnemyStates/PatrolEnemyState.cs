using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemyState : BaseState<EnemyStateMachine.EnemyState>
{
    private EnemyStateMachine _enemy;
    private float walking_radius = 10f;
    private float speed = 1.0f;
    private Vector3 center;
    private bool walking;
    static public Vector3 nextWaypoint;
    private float detection_radius = 4f;
    public PatrolEnemyState(EnemyStateMachine enemy, EnemyStateMachine.EnemyState key = EnemyStateMachine.EnemyState.PATROL): base(key) 
    {
        _enemy = enemy;
        _enemy.Agent.speed = speed;
    }

    public override void EnterState()
    {
        // Set a new center after done chasing, attacking etc
        center = _enemy.transform.position;
        walking = false;

    }

    public override void UpdateState()
    {
        PatrolCycle();

        //_enemy.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

  
    }

    public override void ExitState()
    {

    }

    private void PatrolCycle()
    {
        if (!walking)
        {
            nextWaypoint = GenerateRandomPointIn3DSpace(center, walking_radius);
            _enemy.Agent.isStopped = false;
            _enemy.Agent.SetDestination(nextWaypoint);
            walking = true;
        }
        else
        {
            if (Vector3.Distance(_enemy.transform.position, nextWaypoint) < 0.5)
            {
                walking = false;
            }
        }
    }

    public override EnemyStateMachine.EnemyState GetNextState()
    {
        if (_enemy.PlayerDetected(detection_radius))
        {
            return EnemyStateMachine.EnemyState.CHASE;
        }
        return EnemyStateMachine.EnemyState.PATROL;
    }

    public override void OnTriggerEnter(Collider other){}

    public override void OnTriggerExit(Collider other){}

    public override void OnTriggerStay(Collider other){}

    private Vector3 GenerateRandomPointIn3DSpace(Vector3 centerPosition, float radius)
    {
        float angle = Random.Range(0f, 360f); // Random angle in degrees
        float distance = Random.Range(0f, radius);
        NavMeshHit hit;
        Vector3 randomPoint;
        do
        {
            // Calculate the position relative to the center, restricting the Y coordinate to 0
            randomPoint = centerPosition + new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * distance,
                0,
                Mathf.Sin(angle * Mathf.Deg2Rad) * distance
            );
        } while (!NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas));
        return new Vector3(hit.position.x, randomPoint.y, hit.position.z); ;
    }
}
