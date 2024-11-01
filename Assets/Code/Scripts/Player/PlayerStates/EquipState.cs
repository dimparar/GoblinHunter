using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipState : BaseAnimationState<PlayerAnimationFSM.PlayerAnimation>
{
    public EquipState(PlayerAnimationFSM.PlayerAnimation key = PlayerAnimationFSM.PlayerAnimation.EQUIP)
        : base(key)
    {
        Id = 0;

    }

    public override void EnterState()
    {
        if (WeaponManager._currentWeapon != null)
        {
            if (Id != WeaponManager._currentWeapon.EQUIP.State)
            {
                Id = WeaponManager._currentWeapon.EQUIP.State;
                Duration = WeaponManager._currentWeapon.EQUIP.Duration;
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
        if (GameObject.Find("LoadingScreen") != null)
        {
            GameObject.Find("LoadingScreen").SetActive(false);
        }
        return PlayerAnimationFSM.PlayerAnimation.IDLE;
    }
}
