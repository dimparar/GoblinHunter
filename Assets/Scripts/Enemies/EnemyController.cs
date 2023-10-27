using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, Hittable
{
    [SerializeField]float health;
    [SerializeField] float damage;

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
        }
        else
        {
            health = 0;
            //death
        }
        Debug.Log("Health After: " + health);
    }

    void Start()
    {
        
    }

    void Update()
    {
        /*if ()*/
    }

}
