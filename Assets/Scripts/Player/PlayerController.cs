using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    AudioSource MovementSound;
    AudioSource AttackSound;
    AudioSource EquipSound;

    AudioSource[] soundChannels;


    [Header("Controller")]
    [SerializeField] private float _speed = 5f;
    public float _runningSpeed = 1000f;
    public float _jumpHeight = 2f;
    public float _gravity = -9.8f;

    public PlayerInput _playerInput;
    public PlayerInput.OnFootActions _onFoot;
    public ParticleSystem HitEffect;
    public GameObject HitEffectEmmiter;

    [Header("Sound Effects")]
    [SerializeField] AudioClip walkEffect;
    [SerializeField] AudioClip runEffect;
    [SerializeField] AudioClip attackEffect;
    AudioClip currentClip;

    public void Awake()
    {
        _player = GetComponent<CharacterController>();
    }
    
    public void Start()
    {
        HitEffectEmmiter = GameObject.Find("HitEffect");
        HitEffect = HitEffectEmmiter.GetComponent<ParticleSystem>();
        EventManager.AttackEvent += Attack;
        EventManager.EquipWeaponEvent += PlayEquipSound;
        _player = GetComponent<CharacterController>();
        PlayerMovementManager._isGrounded = true;
        PlayerMovementManager._isRunning = false;
/*        MovementSound = gameObject.GetComponent<AudioSource>();
        MovementSound = gameObject.GetComponent<AudioSource>();*/

        soundChannels = gameObject.GetComponents<AudioSource>();
        MovementSound = soundChannels[0];
        AttackSound = soundChannels[1];
        EquipSound = soundChannels[2];
        
        MovementSound.enabled = false;
        AttackSound.enabled = false;
        EquipSound.enabled = false;
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
            if (_moveDir.x == 0 && _moveDir.z == 0)
            {
                StartCoroutine(Effects.StartFade(MovementSound, 0.2f, 0.0f));

            }
            else if (PlayerMovementManager._isRunning)
            {
                _player.Move(transform.TransformDirection(_moveDir) * _runningSpeed * Time.deltaTime);
                PlaySound(runEffect);
            }
            else
            {
                _player.Move(transform.TransformDirection(_moveDir) * _speed * Time.deltaTime);
                PlaySound(walkEffect);
            }
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
            //Move Attack Sound Effects to animation
            Invoke("PlayAttackSound", WeaponManager._currentWeapon.GetTimeTillHit());
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
            EventManager.PlayerTakeHit();
        }
        else
        {
            health = 0;
            EventManager.PlayerDeath();
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


    //metafora sto player Interact isos i ylopoihsh se kapoio manager
    public void PlayVFX(Vector3 hittablePos)
    {
        HitEffectEmmiter.transform.position = hittablePos + new Vector3(0,1,0);
        HitEffect.Stop(); HitEffect.Play();
    }

    private void PlayEquipSound() 
    {
        EquipSound.enabled = true;
        EquipSound.Play();
    }


    //Tha ginete meso animation
    private void PlayAttackSound()
    {
        AttackSound.enabled = true;
        AttackSound.Play();
    }

    private void PlaySound(AudioClip clip) 
    {
        MovementSound.volume = 1f;
        MovementSound.clip = clip;
        if (!MovementSound.isPlaying)
        {
            MovementSound.Play();
        }

        MovementSound.enabled = true;
    }

}
