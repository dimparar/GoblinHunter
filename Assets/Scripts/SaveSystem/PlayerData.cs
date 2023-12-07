using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public float health;
    public float stamina;
    public int[] weapons;
    public int currentWeapon;
    public int kills;


    public PlayerData(string playerName)
    {
        this.playerName = playerName;
        health = 100;
        stamina = 100;
        weapons = new int[4];
        weapons[0] = 1;
        weapons[1] = 0;
        weapons[2] = 0;
        weapons[3] = 0;
        currentWeapon = 0;
        kills = 0;
    }


    public PlayerData()
    {
    }

    public void SetWeapons(Dictionary<PlayerWeapon, Weapon> weapons,Weapon currentWeapon) 
    {
        var enumerator = weapons.GetEnumerator();
        int index = 0;
        while (enumerator.MoveNext())
        {
            if (enumerator.Current.Value != null)
            {
                this.weapons[index] = 1;
                if (currentWeapon == enumerator.Current.Value)
                {
                    this.currentWeapon = index;
                }
            }
            index++;
        }
    }

    public void IncrementKills()
    {
        kills++;
    }
}


