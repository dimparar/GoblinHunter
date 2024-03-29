
public abstract class Weapon
{


    // Animations
    public AnimationClip IDLE { get; protected set; }
    public AnimationClip EQUIP { get; protected set; }
    public AnimationClip UNEQUIP { get; protected set; }
    public AnimationClip ATTACK { get; protected set; }
    public AnimationClip DEATH { get; protected set; }
    
    protected float weaponDamage;

    protected float staminaConsumption;

    protected float weaponRange;
    
    protected float timeTillHit;
    protected Weapon()
    {

    }

    public abstract PlayerWeapon GetWeapon();

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


    


}
