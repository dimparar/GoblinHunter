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

    public static Dictionary<PlayerWeapon, Weapon> weapons;

    public static void InitWeapons()
    {
        weapons = new Dictionary<PlayerWeapon, Weapon>();
        weapons.Add(PlayerWeapon.HANDS, null);
        weapons.Add(PlayerWeapon.DAGGERS, null);
        weapons.Add(PlayerWeapon.SWORD, null);
        weapons.Add(PlayerWeapon.AXE, null);
        weapons[PlayerWeapon.HANDS] = new Hands();
        _currentWeapon = weapons[PlayerWeapon.HANDS];
        _previousWeapon = _currentWeapon;


    }
    public static void InitWeapons(int[] saveWeapons, int currentWeapon)
    {
        weapons = new Dictionary<PlayerWeapon, Weapon>();
        weapons.Add(PlayerWeapon.HANDS, null);
        weapons.Add(PlayerWeapon.DAGGERS, null);
        weapons.Add(PlayerWeapon.SWORD, null);
        weapons.Add(PlayerWeapon.AXE, null);
        if (saveWeapons != null) { 
        
            if (saveWeapons[0] == 1) 
            {
                weapons[PlayerWeapon.HANDS] = new Hands();
            }
            if (saveWeapons[1] == 1)
            {
                weapons[PlayerWeapon.DAGGERS] = new Daggers();
            }
            if (saveWeapons[2] == 1)
            {
                weapons[PlayerWeapon.SWORD] = new Sword();
            }
            if (saveWeapons[3] == 1)
            {
                weapons[PlayerWeapon.AXE] = new Axe();
            }
            
            var enumerator = weapons.GetEnumerator();
            int index = 0;
            while (enumerator.MoveNext())
            {
                if (currentWeapon == index) 
                {
                    /*_currentWeapon = enumerator.Current.Value;*/
                    ChangeWeapon(enumerator.Current.Value);
                }
                index++;
            }
            /*_previousWeapon = _currentWeapon;*/
            /*ChangeWeapon(currentWeapon);*/

        }

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
        PlayerProfile.UpdateWeapons(weapons,_currentWeapon);

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

        PlayerProfile.UpdateWeapons(weapons,_currentWeapon);
    }


}

