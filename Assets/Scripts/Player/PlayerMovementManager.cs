using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    public static bool _isGrounded { get; set; }
    public static bool _isRunning { get; set; }
    public static bool _isAttacking { get; set; }

    //public bool IsGrounded {get => _isGrounded, }
    private static float attackCooldown = 0;
    private static float timer = 0;

     void Update()
    {
        Debug.Log(_isGrounded);
        timer += Time.deltaTime;
    }

    public static bool CanAttack() 
    {
        if (timer >= attackCooldown)
        {
            attackCooldown = 0;
            return true;
        }
        return false;
    }
    
    public static void AttackPerformed(float time) 
    {
        timer = 0;
        attackCooldown = time+ 0.1f;
    }
}
