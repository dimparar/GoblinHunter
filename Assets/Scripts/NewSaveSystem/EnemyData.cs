using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class EnemyData /*: EnemyData*/
{
    [JsonProperty]
    private string name;
    [JsonProperty]
    private int health;
    [JsonProperty]
    private float[] position;
    [JsonProperty]
    private int type;

    public EnemyData(string name, int health, int type, Vector3 position)
    {
        this.name = name;
        this.health = health;
        this.type = type;
        this.position = new float[3];
        this.position[0] = position.x;
        this.position[1] = position.y;
        this.position[2] = position.z;
    }

    public float[] GetEnemyPosition()
    {
        return position;
    }

    public int GetEnemyType()
    {
      return type;
    }

    public int GetHealth()
    {
        return health;
    }

    public string GetName()
    {
       return name;
    }

    public void SetEnemyPosition(Vector3 position)
    {
        this.position[0]= position.x;
        this.position[1] = position.y;
        this.position[2] = position.z;
    }

    public void SetHealth(int health)
    {
        this.health = health ;
    }

    public void SetName(string name)
    {
        this.name =  name;
    }

    public void SetType(int type)
    {
        this.type = type;
    }
}
