using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinChaseState : BaseState<GoblinStateMachine.GoblinState>
{
    private float speed = 2f;
    private GoblinStateMachine _enemy;
    public GoblinChaseState(GoblinStateMachine enemy, GoblinStateMachine.GoblinState key = GoblinStateMachine.GoblinState.CHASE) : base(key)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy.Agent.isStopped = false;
        _enemy.Agent.speed = speed;
        // Play The Walk Animation
    }

    public override void ExitState()
    {

    }

    public override GoblinStateMachine.GoblinState GetNextState()
    {
        if (_enemy.PlayerDetected(_enemy.combat_radius))
        {
            Debug.Log("Enter Attack");
            return GoblinStateMachine.GoblinState.ATTACK;
        }
        if (!_enemy.PlayerDetected(_enemy.chase_radius))
        {
            return GoblinStateMachine.GoblinState.PATROL;
        }

        return GoblinStateMachine.GoblinState.CHASE;
    }

    public override void OnTriggerEnter(Collider other)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerExit(Collider other)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerStay(Collider other)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        // Don't just chase, find the best way
        _enemy.Agent.SetDestination(_enemy.Player.transform.position);
    }
}
