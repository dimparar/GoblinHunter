using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public class GameData
{
    [JsonProperty]
    public PlayerData playerData;
    [JsonProperty]
    public AreaData[] areasData;
    [JsonProperty]
    public AreaData currentArea;
    [JsonProperty]
    public string time;
    public void SetCurrentAreaData(string areaName) 
    {
        foreach (AreaData area in areasData) 
        {
            if (areaName == area.GetAreaName()) 
            {
                 currentArea = area;
            }
        }
    }

    public void SetCurrentAreaEnemyData(List<EnemyData> enemies)
    {
        if (!currentArea.Visited()) 
        {
            currentArea.SetEnemies(enemies);
            currentArea.SetVisited(true);
        }
    }

    public GameData(string playerName)
    {
        playerData = new PlayerData(playerName);

        areasData = new AreaData[3];
        for (int i = 0; i < areasData.Length; i++) 
        {
            areasData[i] = new AreaData();
        }

        List<Vector3> enemyPositions = new List<Vector3>();
        enemyPositions.Add(new Vector3(48.4f, 2, 13.8f));
        enemyPositions.Add(new Vector3(216.64f, 2, -74.83f));
        enemyPositions.Add(new Vector3(132f, 2, 20f));
        enemyPositions.Add(new Vector3(135f, 2, 11f));
        enemyPositions.Add(new Vector3(220.1f, 2, -79.15f));
        enemyPositions.Add(new Vector3(226f, 2, -80.13f));
        enemyPositions.Add(new Vector3(222.9f, 2, -87f));
        enemyPositions.Add(new Vector3(335.2f, 2, -80.6f));
        enemyPositions.Add(new Vector3(182f, 2, 412));
        enemyPositions.Add(new Vector3(203, 2, 406));
        enemyPositions.Add(new Vector3(193.4f, 2,366.9f));
        enemyPositions.Add(new Vector3(289.53f, 2, 120.72f));
        enemyPositions.Add(new Vector3(137.33f, 2, 14.12f));
        enemyPositions.Add(new Vector3(180, 2, 50f));
        enemyPositions.Add(new Vector3(178f, 2, 222));
        enemyPositions.Add(new Vector3(148.3f, 2, 230.6f));
        enemyPositions.Add(new Vector3(157.4f, 2, 208.2f));
        enemyPositions.Add(new Vector3(198.5f, 2, 116f));
        enemyPositions.Add(new Vector3(198.6f, 2, 434.11f));
        enemyPositions.Add(new Vector3(187.6f, 2, 439.37f));
        enemyPositions.Add(new Vector3(179.33f, 2, 442));
        enemyPositions.Add(new Vector3(129.4f, 2, 448));
        enemyPositions.Add(new Vector3(129.4f, 2, 448));
        enemyPositions.Add(new Vector3(264f, 2, -0.5f));
        enemyPositions.Add(new Vector3(298, 2, 186));
        enemyPositions.Add(new Vector3(273, 2, 192));
        enemyPositions.Add(new Vector3(422, 2, 59));
        enemyPositions.Add(new Vector3(382, 2, -27));
        enemyPositions.Add(new Vector3(422, 2, 72));
        enemyPositions.Add(new Vector3(355, 2, 104));
        enemyPositions.Add(new Vector3(390, 2, 63));
        enemyPositions.Add(new Vector3(44.5f, 2, 145));
        enemyPositions.Add(new Vector3(44.5f, 2, 145));
        enemyPositions.Add(new Vector3(297f, 2, 212.2f));
        enemyPositions.Add(new Vector3(351, 2, 119));

        for (int i = 0; i < enemyPositions.Count-1; i++)
        {
            areasData[1].GetEnemies().Add(new EnemyData("Enemy"+i,100,0, enemyPositions[i]));
        }
        

        areasData[0].SetAreaName("Forest");
        areasData[0].SetPlayerPosition(new Vector3(-1484.8f, 0.5f, 7.53f));
        areasData[0].SetPlayerRotation(new Vector3(0,0,0));
        areasData[0].SetVisited(false);
        areasData[0].SetKey(true);

        areasData[1].SetAreaName("Cave");
        areasData[1].SetPlayerPosition(new Vector3(0, 0, 0));
        areasData[1].SetPlayerRotation(new Vector3(0, 0, 0));
        areasData[0].SetVisited(false);

        areasData[2].SetAreaName("Dungeon");
        areasData[2].SetPlayerPosition(new Vector3(0, 0, 0));
        areasData[2].SetPlayerRotation(new Vector3(0, 0, 0));
        areasData[2].SetPlayerSeed(Random.Range(0,99999));
        areasData[0].SetVisited(false);

        currentArea = areasData[0];
    }
}
