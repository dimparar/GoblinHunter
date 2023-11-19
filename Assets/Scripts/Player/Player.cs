using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Windows;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController), typeof(PlayerController), typeof(PlayerRotate))]
public class Player : MonoBehaviour
{
    public bool _instantiateWeapon = false;
    public PlayerWeapon _currentWeapon;
    public bool _changeWeapon = false;

    public PlayerInput _playerInput;
    public PlayerInput.OnFootActions _onFoot;

    private PlayerController _playerController;
    private PlayerRotate _rotate;
    private PlayerRotate _rotateSmooth;
    private PlayerRotate _currentRotate;
    

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _currentWeapon = PlayerWeapon.HANDS;
        _playerInput = new PlayerInput();
        _onFoot = _playerInput.OnFoot;
        _playerController = GetComponent<PlayerController>();
        _rotate = GetComponents<PlayerRotate>()[0];
        _rotateSmooth = GetComponents<PlayerRotate>()[1];
#if UNITY_EDITOR
        _currentRotate = _rotate;
#else
        _currentRotate = _rotateSmooth;
#endif

        AssignInputs();

    }

    private void Start()
    {
         WeaponManager.initWeapons();
/*        WeaponManager.ChangeWeapon(new Hands());*/
    }

    private void Update()
    {/*
        //_rotate.Rotate(_onFoot.Look.ReadValue<Vector2>());
        _playerController.Move(_onFoot.Movement.ReadValue<Vector2>());

        //Stamina is replenished
        _playerController.ReplenishStamina();

        if (PlayerMovementManager._isGrounded)
        {
            if (_onFoot.Attack.triggered)
            {
                _playerController.Attack();
            }
        }
        */

        _playerController.Move(_onFoot.Movement.ReadValue<Vector2>());
        
        _playerController.ReplenishStamina();
        
/*        if (PlayerMovementManager._isGrounded)
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

    private void LateUpdate()
    {
        _currentRotate.Rotate(_onFoot.Look.ReadValue<Vector2>());

        _currentRotate.CameraTilt(_onFoot.Movement.ReadValue<Vector2>());
       



    }

    private void OnEnable()
    {
        _onFoot.Enable();
    }

    private void OnDisable()
    {
        _onFoot.Disable();
    }

    private void AssignInputs()
    {
        _onFoot.Jump.performed += ctx => _playerController.Jump();
        _onFoot.QuickSave.performed += ctx => SaveSystem.Save();
        _onFoot.Run.performed += ctx => PlayerMovementManager._isRunning = true;
        _onFoot.Run.canceled += ctx => PlayerMovementManager._isRunning = false;



    }

}
