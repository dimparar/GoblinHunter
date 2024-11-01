using UnityEngine;

public class Hands : Weapon
{
    public Hands()
    {
        weaponDamage = 5f;
        staminaConsumption = 5f;
        weaponRange = 4f;
        timeTillHit = 0.375f;
        IDLE = new AnimationClip(Animator.StringToHash("Idle"), 0f);
        EQUIP = new AnimationClip(Animator.StringToHash("Equip"), 0.43f);
        UNEQUIP = new AnimationClip(Animator.StringToHash("Unequip"), 0.3f);
        ATTACK = new AnimationClip(Animator.StringToHash("Punch"), 0.72f);
        DEATH = new AnimationClip(Animator.StringToHash("Death"), 1f);
    }

    public override PlayerWeapon GetWeapon()
    {
        return PlayerWeapon.HANDS;
    }

}
