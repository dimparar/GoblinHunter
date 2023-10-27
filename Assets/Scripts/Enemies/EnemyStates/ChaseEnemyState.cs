using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemyState : BaseState<EnemyStateMachine.EnemyState>
{
    private float chase_radius = 7f;
    private float speed = 2f;
    private EnemyStateMachine _enemy;
    public ChaseEnemyState(EnemyStateMachine enemy, EnemyStateMachine.EnemyState key = EnemyStateMachine.EnemyState.CHASE) : base(key)
    {
        _enemy = enemy;
        _enemy.Agent.speed = speed;
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override EnemyStateMachine.EnemyState GetNextState()
    {
        if (!_enemy.PlayerDetected(chase_radius))
        {
            return EnemyStateMachine.EnemyState.PATROL;
        }
        // TODO: Attack range -> ATTACK_STATE 
        return EnemyStateMachine.EnemyState.CHASE;
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
        // Chase the player
        _enemy.Agent.SetDestination(_enemy.Player.transform.position);
    }
}
