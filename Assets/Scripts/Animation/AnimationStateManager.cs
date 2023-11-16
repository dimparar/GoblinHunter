using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationStateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseAnimationState<EState>> States = new Dictionary<EState, BaseAnimationState<EState>>();

    protected Animator _animator;

    protected AnimationAspectManager AspectManager;

    protected bool TerminateFSM = false;

    protected BaseAnimationState<EState> CurrentState;
    private bool isTransitionState = false;

    protected void ChangeAnimationState(int state)
    {
        //if (state == CurrentState.Id) return;

        /*
        if (_spawnWeapon)
        {
            //When we intend to spawn the next weapon we first check if 
            //the next weapon equip animation has begun
            if (currentClip == _weaponToBeEquiped.EQUIP)
            {
                _spawnWeapon = false;
                _player.WeaponPickUp(_weaponToBeEquiped);
                _player._instantiateWeapon = true;
                _weapon = _weaponToBeEquiped;
                _weaponToBeEquiped = null;
            }
        }
        */

        _animator.CrossFadeInFixedTime(state, 0, 0);
    }


    protected void TransitionToState(EState stateKey)
    {
        isTransitionState = true;

        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();

        //Debug.Log("Enter " + CurrentState.StateKey + "State");

        _animator.CrossFadeInFixedTime(CurrentState.Id, 0, 0);
        isTransitionState = false;
    }

    protected void UpdateAnimation()
    {
        EState nextState = CurrentState.GetNextState();

        if (!isTransitionState)
        {
            if (nextState.Equals(CurrentState.StateKey) || Time.time < CurrentState.LockedStateTime)
            {
                CurrentState.UpdateState();
                if (AspectManager != null)
                {
                    if (AspectManager._changeAspect)
                    {
                        AspectManager._changeAspect = false;
                        _animator.CrossFadeInFixedTime(CurrentState.Id, 0, 0);
                    }
                }
            }
            else
            {
                //Transition to next State
                TransitionToState(nextState);
            }
        }

        //ChangeAnimationState(CurrentState.Id);

    }


    void Start()
    {
        _animator = GetComponent<Animator>();

        CurrentState.EnterState();
    }

    void Update()
    {
        if (!TerminateFSM)
        {
            UpdateAnimation();
        }
    }
}
