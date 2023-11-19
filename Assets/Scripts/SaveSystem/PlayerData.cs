using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int health;
    public int stamina;
    public float[] position;
    public float[] rotation;
    public int[] weapons;
    public int currentWeapon;
    public int kills;
    public string location;

    public PlayerData() 
    {
        playerName = "Test";
        health = 100;
        stamina = 100;
        position =new float[3];
        position[0] = 0;
        position[1] = 0;
        position[2] = 0;
        rotation = new float[3];
        rotation[0] = 0;
        rotation[1] = 0;
        rotation[2] = 0;
        weapons = new int[4];
        weapons[0] = 1;
        weapons[1] = 0;
        weapons[2] = 0;
        weapons[3] = 0;
        currentWeapon = 0;
        kills = 999;
        location = "Serres";
    }

}
