using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GoblinAnimationFSM;

public class GoblinToDeathState : BaseAnimationState<GoblinAnimation>
{
    private Dictionary<Aspects, AnimationClip> toDeathState = new Dictionary<Aspects, AnimationClip>();

    public GoblinToDeathState(AnimationAspectManager _aspectManager, GoblinAnimation key = GoblinAnimation.TO_DEATH)
        : base(key)
    {
        // TODO
        Id = 0;

        AspectManager = _aspectManager;

        toDeathState[Aspects.FRONT] = new AnimationClip(Animator.StringToHash("ToDeath"), 0f);


    }

    private void ChangeAnimation(Aspects aspect)
    {
        Id = toDeathState[aspect].State;
        Duration = toDeathState[aspect].Duration;
    }

    public override void EnterState()
    {
        if (toDeathState[Aspects.FRONT].State != Id)
        {
            //Debug.Log("Change Aspect to " + AspectManager._currentAspectKey);
            ChangeAnimation(Aspects.FRONT);
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


    }

    public override void ExitState()
    {

    }

    public override GoblinAnimation GetNextState()
    {
        // TODO
        return GoblinAnimation.TO_DEATH;

    }
}
