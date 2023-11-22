using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public enum PlayerWeapon
{
    HANDS, SWORD, AXE, SHIELD, DAGGERS
}


public class WeaponManager
{
    public static Weapon _currentWeapon { get; private set; }
    public static Weapon _previousWeapon { get; private set; }
    public static PlayerWeapon _currentWeaponKey { get; private set; }
    public static bool _changeWeapon = false;

    public static Dictionary<PlayerWeapon, Weapon> weapons = new Dictionary<PlayerWeapon, Weapon>();

    public static void initWeapons()
    {
        weapons.Add(PlayerWeapon.HANDS, null);
        weapons.Add(PlayerWeapon.DAGGERS, null);
        weapons.Add(PlayerWeapon.SWORD, null);
        weapons.Add(PlayerWeapon.AXE, null);
        weapons[PlayerWeapon.HANDS] = new Hands();
        _currentWeapon = weapons[PlayerWeapon.HANDS];
        _previousWeapon = _currentWeapon;


    }

    public static void ChangeWeapon(Weapon weapon)
    {
        if (_currentWeapon != null)
        {
            /*_changeWeapon = true;*/

            _previousWeapon = _currentWeapon;
            EventManager.EquipWeapon();
        }
        _currentWeapon = weapon;

        if (weapons[weapon.getWeapon()] == null)
        {
            //EventManager.EquipWeapon();
            weapons[weapon.getWeapon()] = _currentWeapon;
        }
        
    }

    public static void ChangeWeapon(int weaponSlot)
    {
            var enumerator = weapons.GetEnumerator();
            int weaponPosition = 1;
            while (weaponPosition <= weaponSlot)
            {
                enumerator.MoveNext();
                weaponPosition++;
            }

            Weapon weapon = enumerator.Current.Value;
            enumerator.Dispose();

        if (weapon != null && weapon!=_currentWeapon)
        {
            if (_currentWeapon != null)
            {
                /*_changeWeapon = true;*/
                _previousWeapon = _currentWeapon;
                _currentWeapon = weapon;
                EventManager.EquipWeapon();
            }


        }


        }


    }

