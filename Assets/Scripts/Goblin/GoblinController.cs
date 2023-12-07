using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour, Hittable
{
    [SerializeField] private float health = 100;

    public float Health { get => health; set => Health = health; }

    [SerializeField] public float damage = 10f;

    private void Start()
    {
        damage = 10f;
    }

    public void InitEnemy()
    {
        health = 100;
        damage = 3;
    }

    public void TakeDamage(float amount)
    {
        if (health > amount)
        {
            health -= amount;
            PlayerProfile.EnemyDamaged(this.gameObject.name,health);
        }
        else
        {
            EventManager.EnemyDeath(this.gameObject);
            PlayerProfile.EnemyKilled(this.gameObject.name);
            PlayerProfile.EnemyKilled(this.gameObject.name);
            PlayerProfile.IncrementKills();
        }
        Debug.Log("Health After: " + health);
    }

    public bool HasZeroHealth()
    {
        return health == 0;
    }

}
