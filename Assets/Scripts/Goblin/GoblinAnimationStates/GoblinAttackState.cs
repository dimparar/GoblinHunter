using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GoblinAnimationFSM;

public class GoblinAttackState : BaseAnimationState<GoblinAnimation>
{
    private Dictionary<Aspects, AnimationClip> attackState = new Dictionary<Aspects, AnimationClip>();

    private GoblinStateMachine _stateMachine;

    public GoblinAttackState(AnimationAspectManager _aspectManager, GoblinStateMachine stateMachine, GoblinAnimation key = GoblinAnimation.ATTACK)
        : base(key)
    {
        // TODO
        Id = 0;
        AspectManager = _aspectManager;

        _stateMachine = stateMachine;

        attackState[Aspects.FRONT] = new AnimationClip(Animator.StringToHash("Attack"), 1.25f);// 1.25-fast, 3-slow
    }

    private void ChangeAnimation(Aspects aspect)
    {
        Id = attackState[aspect].State;
        Duration = attackState[aspect].Duration;
    }

    public override void EnterState()
    {
        ChangeAnimation(Aspects.FRONT);

        _stateMachine.canEndAttack = false;

        LockState();
    }

    public override void UpdateState()
    {

        if (Time.time >= LockedStateTime)
        {
            if (_stateMachine.IsInCombatRange())
            {
                _stateMachine.canEndAttack = false;
                Debug.Log("Attack end");
                AspectManager._changeAspect = true;
                LockState();
            }
            
            
        }


    }

    public override void ExitState()
    {
        //_stateMachine.isAttacking = false;
    }

    public override GoblinAnimation GetNextState()
    {

        //if (_stateMachine.isAttacking)
        //{
        //    return GoblinAnimation.ATTACK;
        //}

        if (_stateMachine.Agent.velocity.magnitude > 0)
        {
            //Debug.Log("Animation: Enter Walk State");
            return GoblinAnimation.WALK;
        }

        return GoblinAnimation.ATTACK;

    }
}
