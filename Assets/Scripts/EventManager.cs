using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class EventManager /*: MonoBehaviour*/
{
    public static event Action AttackEvent;

    public static event Action EquipWeaponEvent;
    
    public static event Action<Hittable> EnemyHitEvent;

    public static event Action PlayerHitEvent;

    public static event Action PlayerDeathEvent;


    public static void AttackPerformed()
    {
        AttackEvent?.Invoke();
    }

    public static void EquipWeapon()
    {
        EquipWeaponEvent?.Invoke();
    }

    public static void EnemyHitPerformed(Hittable enemy)
    { 
        EnemyHitEvent?.Invoke(enemy);
        enemy.TakeDamage(WeaponManager._currentWeapon.GetWeaponDamage());
    }
    public static void PlayerDeath()
    {
        PlayerDeathEvent?.Invoke();
    }

    internal static void PlayerTakeHit()
    {
        PlayerHitEvent?.Invoke();
    }
}