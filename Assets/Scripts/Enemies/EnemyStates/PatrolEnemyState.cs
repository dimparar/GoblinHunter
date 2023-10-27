using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState<EnemyStateMachine.EnemyState>
{
    private EnemyStateMachine _enemy;
    private float walking_radius = 10;
    private float speed = 1.0f;
    private Vector3 center;
    private bool walking;
    private Vector3 nextWaypoint;
    private float detection_radius = 4f;
    public PatrolState(EnemyStateMachine enemy, EnemyStateMachine.EnemyState key = EnemyStateMachine.EnemyState.PATROL): base(key) 
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

        if (_enemy.transform.eulerAngles.y < 300 && _enemy.transform.eulerAngles.y > 240)
        {
            //TODO
            AnimationAspectManager._currentAspectKey = Aspects.LEFT;
        }
        
        if (_enemy.transform.eulerAngles.y > 60 && _enemy.transform.eulerAngles.y < 130)
        {
            //TODO
            AnimationAspectManager._currentAspectKey = Aspects.RIGHT;
        }

        if (_enemy.transform.eulerAngles.y < 60 || _enemy.transform.eulerAngles.y > 300)
        {
            //TODO
            AnimationAspectManager._currentAspectKey = Aspects.FRONT;
        }
        
        if (_enemy.transform.eulerAngles.y > 130 && _enemy.transform.eulerAngles.y < 240)
        {
            //TODO
            AnimationAspectManager._currentAspectKey = Aspects.BACK;
        }
    }

    public override void ExitState()
    {

    }

    private void PatrolCycle()
    {
        if (!walking)
        {
            nextWaypoint = GenerateRandomPointIn3DSpace(center, walking_radius);
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

        // Calculate the position relative to the center, restricting the Y coordinate to 0
        Vector3 randomPoint = centerPosition + new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad) * distance,
            0, // Restricting the Y coordinate to 0
            Mathf.Sin(angle * Mathf.Deg2Rad) * distance
        );

        // TODO
        // Check if the point is accessible to the room
        return randomPoint;
    }
}
