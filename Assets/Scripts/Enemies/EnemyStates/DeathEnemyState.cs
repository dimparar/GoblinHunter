using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEnemyState : BaseState<EnemyStateMachine.EnemyState>
{


    public DeathEnemyState(EnemyStateMachine.EnemyState key = EnemyStateMachine.EnemyState.DEAD)
        : base(key) { }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override EnemyStateMachine.EnemyState GetNextState()
    {
        // TODO
        return EnemyStateMachine.EnemyState.DEAD;
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
        /*        this._enemy.transform.position = Vector3.MoveTowards(this._enemy.transform.position, targetObject.transform.position, 10*Time.deltaTime);
        */
    }
}
