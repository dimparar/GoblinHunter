using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttackLogicState : BaseState<GoblinStateMachine.GoblinState>
{
    private int _currentWaypointIndex;
    private GoblinStateMachine _enemy;
    private float attackWeight = 70f;   // Initial weight for attacking
    private float blockWeight = 20f;    // Initial weight for blocking
    private float continueAttackWeight = 10f;  // Initial weight for continuing to attack
    private float weightChangeRate = 5f; // Rate at which weights change (adjust this as needed)

    public GoblinAttackLogicState(GoblinStateMachine enemy, GoblinStateMachine.GoblinState key = GoblinStateMachine.GoblinState.ATTACK) : base(key)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy.StartAttack();

        //_enemy.isAttacking = true;
        //_enemy.Agent.isStopped = true;
        //_enemy.Agent.speed = 0;
    }

    public override void ExitState()
    {
        _enemy.isAttacking = false;

    }

    public override GoblinStateMachine.GoblinState GetNextState()
    {
        //Debug.Log("endAttack: " + _enemy.canEndAttack);

        if (!_enemy.PlayerDetected(_enemy.combat_radius) && _enemy.canEndAttack)
        {
            Debug.Log("Enter Chase");
            return GoblinStateMachine.GoblinState.CHASE;
        }

        return GoblinStateMachine.GoblinState.ATTACK;
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

        //if (!_enemy.isAttacking)
        //{
        //    _enemy.StartAttack();
        //}

    }
}
