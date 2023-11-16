using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GoblinAnimationFSM;

public class GoblinIdleState : BaseAnimationState<GoblinAnimation>
{
    private Dictionary<Aspects, AnimationClip> idleState = new Dictionary<Aspects, AnimationClip>();

    private GoblinStateMachine _stateMachine;

    public GoblinIdleState(AnimationAspectManager _aspectManager, GoblinStateMachine stateMachine, GoblinAnimation key = GoblinAnimation.IDLE)
        : base(key)
    {
        // TODO
        Id = 0;

        AspectManager = _aspectManager;
        _stateMachine = stateMachine;

        idleState[Aspects.FRONT] = new AnimationClip(Animator.StringToHash("Idle"), 0f);
        idleState[Aspects.LEFT] = new AnimationClip(Animator.StringToHash("IdleLeft"), 0f);
        idleState[Aspects.RIGHT] = new AnimationClip(Animator.StringToHash("IdleRight"), 0f);
        idleState[Aspects.BACK] = new AnimationClip(Animator.StringToHash("IdleBack"), 0f);

        idleState[Aspects.RIGHT_FRONT] = new AnimationClip(Animator.StringToHash("IdleLeftFront"), 0f);
        idleState[Aspects.RIGHT_BACK] = new AnimationClip(Animator.StringToHash("IdleLeftBack"), 0f);
        idleState[Aspects.LEFT_FRONT] = new AnimationClip(Animator.StringToHash("IdleRightFront"), 0f);
        idleState[Aspects.LEFT_BACK] = new AnimationClip(Animator.StringToHash("IdleRightBack"), 0f);

    }

    private void ChangeAnimation(Aspects aspect)
    {
        Id = idleState[aspect].State;
        Duration = idleState[aspect].Duration;
    }

    public override void EnterState()
    {
        if (idleState[AspectManager._currentAspectKey].State != Id)
        {
            //Debug.Log("Change Aspect to " + AspectManager._currentAspectKey);
            ChangeAnimation(AspectManager._currentAspectKey);
        }

    }

    public override void UpdateState()
    {
        if (idleState[AspectManager._currentAspectKey].State != Id)
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
            return GoblinAnimation.ATTACK;
        }

        if (_stateMachine.Agent.velocity.magnitude > 0)
        {
            return GoblinAnimation.WALK;
        }
        
        return GoblinAnimation.IDLE;

    }
}
