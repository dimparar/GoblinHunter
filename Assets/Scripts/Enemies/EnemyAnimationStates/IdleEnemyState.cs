using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleEnemyState : BaseAnimationState<EnemyAnimationFSM.EnemyAnimation>
{
    private Dictionary<Aspects, AnimationClip> idleState = new Dictionary<Aspects, AnimationClip>();

    public IdleEnemyState(AnimationAspectManager _aspectManager, EnemyAnimationFSM.EnemyAnimation key = EnemyAnimationFSM.EnemyAnimation.IDLE)
        : base(key)
    {
        // TODO
        Id = 0;

        AspectManager = _aspectManager;

        idleState[Aspects.FRONT] = new AnimationClip(Animator.StringToHash("Idle"), 0f);
        idleState[Aspects.LEFT] = new AnimationClip(Animator.StringToHash("IdleLeft"), 0f);
        idleState[Aspects.RIGHT] = new AnimationClip(Animator.StringToHash("IdleRight"), 0f);
        idleState[Aspects.BACK] = new AnimationClip(Animator.StringToHash("IdleBack"), 0f);

        idleState[Aspects.LEFT_FRONT] = new AnimationClip(Animator.StringToHash("IdleLeftFront"), 0f);
        idleState[Aspects.LEFT_BACK] = new AnimationClip(Animator.StringToHash("IdleLeftBack"), 0f);
        idleState[Aspects.RIGHT_FRONT] = new AnimationClip(Animator.StringToHash("IdleRightFront"), 0f);
        idleState[Aspects.RIGHT_BACK] = new AnimationClip(Animator.StringToHash("IdleRightBack"), 0f);

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


        //TODO
        //if (WeaponManager._currentWeapon != null)
        //{
        //    if (Id != WeaponManager._currentWeapon.ATTACK.State)
        //    {
        //        Id = WeaponManager._currentWeapon.ATTACK.State;
        //        Duration = WeaponManager._currentWeapon.ATTACK.Duration;

        //    }
        //}

        //LockState();

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

    public override EnemyAnimationFSM.EnemyAnimation GetNextState()
    {
        // TODO

        return EnemyAnimationFSM.EnemyAnimation.IDLE;

    }
}
