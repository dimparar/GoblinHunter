using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//Pithano Onoma PlayerController
public class PlayerController : MonoBehaviour, Hittable
{
    private CharacterController _player;
    public Vector3 _moveDir;
    private Vector3 _velocity;

    [SerializeField] public float health;
    //stamina
    [SerializeField] private float maxStamina;
    private float regenRate = 5;
    /*    private float lastRegenTime =0;*/
    [SerializeField] public  float stamina;
    private float defense=1.0f;

    [Header("Controller")]
    [SerializeField] private float _speed = 5f;
    public float _runningSpeed = 1000f;
    public float _jumpHeight = 2f;
    public float _gravity = -9.8f;

    public PlayerInput _playerInput;
    public PlayerInput.OnFootActions _onFoot;

    public void Awake()
    {
        _player = GetComponent<CharacterController>();
    }
    
    public void Start()
    {
        EventManager.AttackEvent += Attack;
        _player = GetComponent<CharacterController>();
        PlayerMovementManager._isGrounded = true;
        PlayerMovementManager._isRunning = false;
    }

    void Update()
    {
 /*       _playerController.Move(_onFoot.Movement.ReadValue<Vector2>());
        _playerController.ReplenishStamina();
        if (PlayerMovementManager._isGrounded)
        {
            if (_onFoot.Attack.triggered)
            {
                if (PlayerMovementManager.CanAttack())
                {
                   PlayerMovementManager.AttackPerformed(WeaponManager._currentWeapon.ATTACK.Duration);
                   _playerController.Attack();
                }
            }
        }*/
    }

    public virtual void Move(Vector2 input)
    {
        _moveDir.x = input.x;
        _moveDir.z = input.y;

        if (PlayerMovementManager._isGrounded)
        {
            if (PlayerMovementManager._isRunning)
            {
                _player.Move(transform.TransformDirection(_moveDir) * _runningSpeed * Time.deltaTime);
            }
            else
            {
                _player.Move(transform.TransformDirection(_moveDir) * _speed * Time.deltaTime);
            }
        }
        else
        {

        }

        _velocity.y += _gravity * Time.deltaTime;
        if (PlayerMovementManager._isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }

    public void Attack()
    {
        if (stamina>WeaponManager._currentWeapon.GetStaminaConsumption())
        {
            /*PlayerMovementManager._isAttacking = true;*/
            stamina -= WeaponManager._currentWeapon.GetStaminaConsumption();
        }

    }


    public void Jump()
    {
        if (PlayerMovementManager._isGrounded)
        {
            _velocity.y = Mathf.Sqrt(-3.0f * _gravity * _jumpHeight);
        }
    }

    public void TakeDamage(float amount)
    {
        if (health > amount)
        {
            Debug.Log("Health Before: " + health);
            health -= amount*defense;
        }
        else
        {
            health = 0;
            //death
        }
        Debug.Log("Health After: " + health);
    }

    
    public void ReplenishStamina() 
    {
        if (stamina<maxStamina) 
        {
            //frame rate independent stamina regen
            stamina = Mathf.Clamp(stamina + Time.deltaTime*regenRate , 0, maxStamina);
        }
    }


}
