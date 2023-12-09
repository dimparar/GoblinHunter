using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseAnimationState<PlayerAnimationFSM.PlayerAnimation>
{  
    public DeathState(PlayerAnimationFSM.PlayerAnimation key = PlayerAnimationFSM.PlayerAnimation.DEATH)
        : base(key)
    {
        // TODO
        Id = 0;

    }

    public override void EnterState()
    {
        if (WeaponManager._currentWeapon != null)
        {
            if (Id != WeaponManager._currentWeapon.DEATH.State)
            {
                Id = WeaponManager._currentWeapon.DEATH.State;
                Duration = WeaponManager._currentWeapon.DEATH.Duration;
            }

        }


        LockState();

    }

    public override void UpdateState()
    {  

    }

    public override void ExitState()
    {

    }

    public override PlayerAnimationFSM.PlayerAnimation GetNextState()
    {
        return PlayerAnimationFSM.PlayerAnimation.IDLE;
    }

}
