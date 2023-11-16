using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GoblinAnimationFSM;

public class GoblinWalkState : BaseAnimationState<GoblinAnimation>
{
    private Dictionary<Aspects, AnimationClip> walkState = new Dictionary<Aspects, AnimationClip>();

    private GoblinStateMachine _stateMachine;

    public GoblinWalkState(AnimationAspectManager _aspectManager, GoblinStateMachine stateMachine, GoblinAnimation key = GoblinAnimation.WALK)
        : base(key)
    {
        // TODO
        Id = 0;

        AspectManager = _aspectManager;
        _stateMachine = stateMachine;

        walkState[Aspects.FRONT] = new AnimationClip(Animator.StringToHash("WalkFront"), 0f);
        walkState[Aspects.LEFT] = new AnimationClip(Animator.StringToHash("WalkLeft"), 0f);
        walkState[Aspects.RIGHT] = new AnimationClip(Animator.StringToHash("WalkRight"), 0f);
        walkState[Aspects.BACK] = new AnimationClip(Animator.StringToHash("WalkBack"), 0f);

        walkState[Aspects.RIGHT_FRONT] = new AnimationClip(Animator.StringToHash("WalkLeftFront"), 0f);
        walkState[Aspects.RIGHT_BACK] = new AnimationClip(Animator.StringToHash("WalkLeftBack"), 0f);
        walkState[Aspects.LEFT_FRONT] = new AnimationClip(Animator.StringToHash("WalkRightFront"), 0f);
        walkState[Aspects.LEFT_BACK] = new AnimationClip(Animator.StringToHash("WalkRightBack"), 0f);

    }

    private void ChangeAnimation(Aspects aspect)
    {
        Id = walkState[aspect].State;
        Duration = walkState[aspect].Duration;
    }

    public override void EnterState()
    {
        if (walkState[AspectManager._currentAspectKey].State != Id)
        {
            //Debug.Log("Change Aspect to " + AspectManager._currentAspectKey);
            ChangeAnimation(AspectManager._currentAspectKey);
        }

    }

    public override void UpdateState()
    {
        if (_stateMachine.CurrentState.StateKey == GoblinStateMachine.GoblinState.CHASE)
        {
            AspectManager.disableAspects = true;
        }
        else
        {
            AspectManager.disableAspects = false;
        }

        if (walkState[AspectManager._currentAspectKey].State != Id)
        {
            //Debug.Log("Change Aspect to " + AspectManager._currentAspectKey);
            ChangeAnimation(AspectManager._currentAspectKey);
            AspectManager._changeAspect = true;

        }

    }

    public override void ExitState()
    {
    
    }

    public override GoblinAnimation GetNextState()
    {
        // TODO
        if (_stateMachine.isAttacking)
        {
            Debug.Log("Animation: Enter Attack State");
            return GoblinAnimation.ATTACK;
        }

        if (_stateMachine.Agent.velocity.magnitude > 0)
        {
            return GoblinAnimation.WALK;
        }

        return GoblinAnimation.IDLE;

    }
}
