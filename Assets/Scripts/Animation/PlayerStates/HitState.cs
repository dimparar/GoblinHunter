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
        // TODO
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


        LockState();



    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;



    }

    public override void ExitState()
    {

    }

    public override PlayerAnimationFSM.PlayerAnimation GetNextState()
    {
        /*        // TODO
                if (PlayerMovementManager._isAttacking)
                {
                    PlayerMovementManager._isAttacking = false;
                    return PlayerAnimationFSM.PlayerAnimation.ATTACK;
                }*/
        if (timer > staggerTime)
        {
            return PlayerAnimationFSM.PlayerAnimation.IDLE;
        }
        return PlayerAnimationFSM.PlayerAnimation.HIT;

    }
}
