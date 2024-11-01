using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : BaseAnimationState<PlayerAnimationFSM.PlayerAnimation>
{

    private float staggerTime;
    private float timer;
    public HitState(PlayerAnimationFSM.PlayerAnimation key = PlayerAnimationFSM.PlayerAnimation.IDLE)
          : base(key)
    {
        Id = 0;

    }

    public override void EnterState()
    {
        if (WeaponManager._currentWeapon != null)
        {
            if (Id != WeaponManager._currentWeapon.IDLE.State)
            {
                Id = WeaponManager._currentWeapon.IDLE.State;
                Duration = WeaponManager._currentWeapon.IDLE.Duration;
            }

        }
        timer = 0;
        staggerTime = 0.7f;


        LockState();



    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;

    }

    public override void ExitState()
    {
        timer = 0;
    }

    public override PlayerAnimationFSM.PlayerAnimation GetNextState()
    {
        if (timer > staggerTime)
        {
            return PlayerAnimationFSM.PlayerAnimation.IDLE;
        }
        return PlayerAnimationFSM.PlayerAnimation.HIT;

    }
}
