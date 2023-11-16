using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyAnimationFSM;

public class GoblinAnimationFSM : AnimationStateManager<GoblinAnimationFSM.GoblinAnimation>
{
    private GoblinStateMachine stateMachine;

    public enum GoblinAnimation
    {
        IDLE, WALK, HIT, TO_DEATH, ATTACK
    }

    void InitStates()
    {
        States.Add(GoblinAnimation.IDLE, new GoblinIdleState(AspectManager, stateMachine));
        States.Add(GoblinAnimation.WALK, new GoblinWalkState(AspectManager, stateMachine));
        States.Add(GoblinAnimation.HIT, new GoblinHitState(AspectManager, stateMachine));
        States.Add(GoblinAnimation.TO_DEATH, new GoblinToDeathState(AspectManager));
        States.Add(GoblinAnimation.ATTACK, new GoblinAttackState(AspectManager, stateMachine));

    }

    private void Awake()
    {
        stateMachine = GetComponent<GoblinStateMachine>();
        AspectManager = GetComponent<AnimationAspectManager>();
        EventManager.EnemyHitEvent += GetHit;
        EventManager.EnemyDeathEvent += ToDeathAnimation;
        InitStates();
        CurrentState = States[GoblinAnimation.IDLE];
    }

    public Transform GetTransform()
    {
        return transform;
    }

    
    public void GetHit(Hittable enemy)
    {
        if (gameObject.GetComponent<Hittable>() == enemy && CurrentState.StateKey != GoblinAnimation.TO_DEATH)
        {
            TransitionToState(GoblinAnimation.HIT);
        }
    }
    
    
    public void ToDeathAnimation(Hittable enemy)
    {
        if (gameObject.GetComponent<Hittable>() == enemy && !TerminateFSM)
        {
            TransitionToState(GoblinAnimation.TO_DEATH);
            TerminateFSM = true;
        }
    }

    public void EndAttackAnimation()
    {
        stateMachine.isAttacking = false;


    }

    private void LateUpdate()
    {
        //if (CurrentState.StateKey == GoblinAnimation.WALK)
        //{
        //    _animator.speed = stateMachine.Agent.desiredVelocity.magnitude * 0.5f;

        //}
        //else
        //{
        //    _animator.speed = 1f;
        //}

    }

}