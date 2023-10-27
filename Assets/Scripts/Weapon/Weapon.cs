using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{


    // Animations
    public AnimationClip IDLE { get; protected set; }
    public AnimationClip EQUIP { get; protected set; }
    public AnimationClip UNEQUIP { get; protected set; }
    public AnimationClip ATTACK { get; protected set; }
    
    protected float weaponDamage;

    protected float staminaConsumption;

    protected float weaponRange;
    
    protected float timeTillHit;
    protected Weapon()
    {

    }
    public float GetWeaponDamage() 
    {
        return weaponDamage;
    }
    public void SetWeaponDamage(float amount)
    {
        weaponDamage = amount;
    }

    public float GetStaminaConsumption()
    {
        return staminaConsumption;
    }

    public float GetWeaponRange()
    {
        return weaponRange;
    }

    public float GetTimeTillHit() 
    {
        return timeTillHit;
    }
    public abstract PlayerWeapon getWeapon();

    


}
