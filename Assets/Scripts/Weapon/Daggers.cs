using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daggers : Weapon
{
    public Daggers()
    {
        weaponDamage = 20f;
        staminaConsumption = 7f;
        weaponRange = 2f;
        timeTillHit = 0.332f;
        IDLE = new AnimationClip(Animator.StringToHash("IdleDaggers"), 0f);
        EQUIP = new AnimationClip(Animator.StringToHash("EquipDaggers"), 0.43f);
        UNEQUIP = new AnimationClip(Animator.StringToHash("UnequipDaggers"), 0.43f);
        ATTACK = new AnimationClip(Animator.StringToHash("AttackDaggers"), 0.684f);
    }


    public override PlayerWeapon getWeapon()
    {
        return PlayerWeapon.DAGGERS;
    }
}
