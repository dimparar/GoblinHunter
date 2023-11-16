using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();

    public BaseState<EState> CurrentState { get; protected set; }

    protected bool isTransitionState = false;

    protected bool TerminateFSM;

    void Start()
    {
        CurrentState.EnterState();
    }

    void Update()
    {
        if (!TerminateFSM)
        {
            EState nextState = CurrentState.GetNextState();
        
            if (!isTransitionState)
            {
                if (nextState.Equals(CurrentState.StateKey))
                {
                    CurrentState.UpdateState();
                }
                else
                {
                    //Transition to next State
                    TransitionToState(nextState);

                }
            }
        }

    }

    protected void TransitionToState(EState stateKey)
    {
        isTransitionState = true;
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        isTransitionState = false;
    }

    void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);  
    }
    
    void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);  
    }
    
    void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);  
    }
}
