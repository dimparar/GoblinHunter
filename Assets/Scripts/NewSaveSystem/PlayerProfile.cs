using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public static class PlayerProfile
{
    public static GameData gameData;
   
    public static void NewGameData(string playerName) 
    {
       gameData  = new GameData(playerName);
    }

    public static void LoadGameData(GameData loadGame) 
    {
        gameData = loadGame;
    }

    public static void UpdateCurrentArea() 
    {
 
        gameData.SetCurrentAreaData(SceneManager.GetActiveScene().name);
    }

    public static void UpdatePlayerHealth(float health)
    {
        gameData.playerData.health = health;
    }

    public static void UpdateWeapons(Dictionary<PlayerWeapon, Weapon> weapons, Weapon currentWeapon) 
    {
        gameData.playerData.SetWeapons(weapons, currentWeapon);
    }

    public static void UpdatePosition(Vector3 position)
    {
        gameData.currentArea.SetPlayerPosition(position);
    }

    public static void UpdateRotation(Vector3 rotation) 
    {
        gameData.currentArea.SetPlayerRotation(rotation);
    }

    public static void EnemyKilled(string enemyName) 
    {
        gameData.currentArea.RemoveEnemy(enemyName);
    }

    public static void EnemyDamaged(string enemyName, float health) 
    {
        gameData.currentArea.DamageEnemy(enemyName,(int)health);
    }

    public static void IncrementKills()
    {
        gameData.playerData.IncrementKills();
    }

    public static void SetCurrentAreaEnemyList(List<EnemyData> enemies)
    {
        gameData.SetCurrentAreaEnemyData(enemies);
    }

    public static void SetCurrentArea(string areaName)
    {
       gameData.SetCurrentAreaData(areaName);
    }
}
