using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Search;
using UnityEngine;

public class EnemyController : MonoBehaviour, Hittable
{
    [SerializeField] private float health = 100;

    public float Health { get => health; set => Health = health; }

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
            MoveBack();
            /*ShowHitEffect();*/
        }
        else
        {
            health = 0;
            //death
        }
        Debug.Log("Health After: " + health);
    }

    private void MoveBack()
    {
        gameObject.transform.Translate(100 * Time.deltaTime * GameObject.Find("Main Camera").GetComponent<Transform>().forward, Space.World);
    }

    void Start()
    {
        
    }

    void Update()
    {
        /*if ()*/
    }


}
