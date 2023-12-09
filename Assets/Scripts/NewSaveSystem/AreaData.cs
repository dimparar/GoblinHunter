using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public class AreaData /*: AreaData*/
{
    [JsonProperty]
    private string areaName;
    [JsonProperty]
    private float[] playerPosition;
    [JsonProperty]
    private float[] playerRotation;
    [JsonProperty]
    private int seed;
    [JsonProperty]
    private List<EnemyData> enemies;
<<<<<<< HEAD
    [JsonProperty]
    private bool visited;
=======
>>>>>>> 9d0a6069372e1347f40029d2ddcfcef97ca5805b

    public AreaData() 
    {
        enemies = new List<EnemyData>();
        playerPosition = new float[3];
        playerRotation = new float[3];
    }
    public void SetAreaName(string name)
    {
        areaName = name;
    }

    public void SetPlayerPosition(Vector3 position)
    {
        playerPosition[0] = position.x;
        playerPosition[1] = position.y;
        playerPosition[2] = position.z;
    }

    public void SetPlayerRotation(Vector3 rotation)
    {
        playerRotation[0] = rotation.x;
        playerRotation[1] = rotation.y;
        playerRotation[2] = rotation.z;
    }

    public void SetEnemies(List<EnemyData>enemies)
    {
<<<<<<< HEAD
        foreach(EnemyData enemy in enemies) 
        {
            this.enemies.Add(enemy);
        }
=======
        this.enemies = enemies;
>>>>>>> 9d0a6069372e1347f40029d2ddcfcef97ca5805b
    }

    public void SetPlayerSeed(int i)
    {
        seed = i;
    }
<<<<<<< HEAD
    public void SetVisited(bool b)
    {
        visited = b;
    }

    public bool Visited() 
    {
        return visited;
    }
=======
>>>>>>> 9d0a6069372e1347f40029d2ddcfcef97ca5805b
    public string GetAreaName()
    {
       return areaName;
    }

    public List<EnemyData> GetEnemies()
    {
        return enemies;
    }

    public float[] GetPlayerPosition()
    {
        return playerPosition;
    }

    public float[] GetPlayerRotation()
    {
        return playerRotation;
    }

    public int GetPlayerSeed()
    {
        return seed;
    }

    public bool RemoveEnemy(string enemyName)
    {
        if (enemies != null || enemies.Count != 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].GetName() == enemyName)
                {
                    enemies.Remove(enemies[i]);
                    return true;
                }

            }
        }
        else 
        {
            Debug.Log("Enemy List not used in current Area");
        }
        return false;
    }

    public void DamageEnemy(string enemyName, int health)
    {
        if (enemies != null || enemies.Count != 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].GetName() == enemyName)
                {
                    enemies[i].SetHealth(health);
                }

            }
        }
    }
}
