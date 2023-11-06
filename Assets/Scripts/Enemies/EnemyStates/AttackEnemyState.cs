using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyState : BaseState<EnemyStateMachine.EnemyState>
{
    private int _currentWaypointIndex;
    private EnemyStateMachine _enemy;
    private float combat_radius = 2f;
    private float attackWeight = 70f;   // Initial weight for attacking
    private float blockWeight = 20f;    // Initial weight for blocking
    private float continueAttackWeight = 10f;  // Initial weight for continuing to attack
    private float weightChangeRate = 5f; // Rate at which weights change (adjust this as needed)

    public AttackEnemyState(EnemyStateMachine enemy, EnemyStateMachine.EnemyState key = EnemyStateMachine.EnemyState.ATTACK) : base(key) 
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {

    }

    public override EnemyStateMachine.EnemyState GetNextState()
    {
        if (!_enemy.PlayerDetected(combat_radius))
        {
            return EnemyStateMachine.EnemyState.CHASE;
        }
        return EnemyStateMachine.EnemyState.ATTACK;
    }

    public override void OnTriggerEnter(Collider other)
    {

    }

    public override void OnTriggerExit(Collider other)
    {

    }

    public override void OnTriggerStay(Collider other)
    {

    }

    public override void UpdateState()
    {
        // if enemy is taking frequent damage: block weight += weightChangeRate

        // if enemy is low on health: block weight += weightChangeRate

        // if enemy is hitting the player in sequence: attack weight += weightChangeRate

        // Normalize the weights to ensure they add up to 100
        /*float totalWeight = attackWeight + blockWeight + continueAttackWeight;
        attackWeight /= totalWeight * 100;
        blockWeight /= totalWeight * 100;
        continueAttackWeight /= totalWeight * 100;

        // Randomly select an action based on the adjusted weights
        float randomValue = Random.Range(0f, 100f);

        if (randomValue <= attackWeight) // Attack
        {
*//*            enemy.AttackPlayer();
*//*        }
        else if (randomValue <= attackWeight + blockWeight) // Block
        {
*//*            enemy.Block();
*//*        }
        else // Continue Attacking
        {
*//*            enemy.ContinueAttacking();
*//*      }*/
        _enemy.Agent.isStopped = true;
        if (!_enemy.isAttacking)
        {
            _enemy.StartCoroutine(_enemy.AttackAndWait());
        }
    }

}
