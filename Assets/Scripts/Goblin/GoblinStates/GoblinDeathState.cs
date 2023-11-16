using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDeathState : BaseState<GoblinStateMachine.GoblinState>
{
    private GoblinStateMachine _enemy;

    public GoblinDeathState(GoblinStateMachine enemy, GoblinStateMachine.GoblinState key = GoblinStateMachine.GoblinState.DEAD)
        : base(key)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy.Agent.speed = 0;
    }

    public override void ExitState()
    {

    }

    public override GoblinStateMachine.GoblinState GetNextState()
    {
        // TODO
        return GoblinStateMachine.GoblinState.DEAD;
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
