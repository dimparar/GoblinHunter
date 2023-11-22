using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraHolder;

    [SerializeField]
    private float rayMaxDistance = 2f;

    [SerializeField]
    private LayerMask mask;

    private PlayerUI _playerUI;
    private Player _player;

    private OutlineEffect outline;
    private OutlineEffect3D outline3D;

    Hittable hitTarget;
    Transform hittablePos;
    GameObject target;
    private void Awake()
    {
        _playerUI = GetComponent<PlayerUI>();
        _player = GetComponent<Player>();
    }

    /*    private void Start()
        {
            WeaponManager.initWeapons();
        }*/

    private void checkInteraction()
    {
        _playerUI.updateText(string.Empty);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, rayMaxDistance, mask))
        {

            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {

                Debug.DrawRay(ray.origin, ray.direction * rayMaxDistance, Color.red);
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();


                // 2D object
                if (interactable.GetComponent<OutlineEffect>() != null)
                {
                    outline = interactable.GetComponent<OutlineEffect>();
                    outline._isSelected = true;
                }


                // 3D object
                //if (interactable.GetComponent<OutlineEffect3D>() != null)
                //{
                //    outline3D = interactable.GetComponent<OutlineEffect3D>();
                //    outline3D._isSelected = true;
                //}


                // Player UI Update
                _playerUI.crosshairInteraction();
                _playerUI.updateText(interactable.GetPromptMessage());

                if (_player._onFoot.Interact.triggered)
                {
                    /*                if (interactable.GetComponent<Weapon>() != null)
                                    {
                                        _player._changeWeapon = true;
                                        //! allagi isos
                                        _player._currentWeapon = PlayerWeapon.SWORD;
                                    }
                    */
                    interactable.Interact();
                }
            }
        }
        else
        {
            if (outline != null)
            {
                outline._isSelected = false;
                outline = null;
            }

            //if (outline3D != null)
            //{
            //    outline3D._isSelected = false;
            //    outline3D = null;
            //}

            _playerUI.crosshairNoInteraction();
            Debug.DrawRay(ray.origin, ray.direction * rayMaxDistance, Color.green);
        }

    }

    private void CheckHit(float weaponRange)
    {
        if (_player._onFoot.Attack.triggered)
        {
            if (PlayerMovementManager.CanAttack())
            {
                PlayerMovementManager.AttackPerformed(WeaponManager._currentWeapon.ATTACK.Duration);
                EventManager.AttackPerformed();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, weaponRange, mask))
                {
                    if (hitInfo.collider.GetComponent<Hittable>() != null)
                    {
                        Debug.DrawRay(ray.origin, ray.direction * rayMaxDistance, Color.cyan);


                        target = hitInfo.collider.gameObject;

                        /*                        Hittable hittable = hitInfo.collider.GetComponent<Hittable>();
                                                Transform hittablePos = hitInfo.collider.GetComponent<Transform>();*/

                        // Player UI Update

                        //_playerUI.crosshairInteraction();
                        //hittable.TakeDamage(WeaponManager._currentWeapon.GetWeaponDamage());

                        _playerUI.crosshairInteraction();
                        /*                        hitTarget = hittable;                        
                                                this.hittablePos = hittablePos;*/
                        Invoke("InvokeEnemyHit", WeaponManager._currentWeapon.GetTimeTillHit());
                        
                    }
                }
                else
                {
                    //_playerUI.crosshairNoInteraction();
                    Debug.DrawRay(ray.origin, ray.direction * rayMaxDistance, Color.yellow);
                }
            }

        }


    }


    void Update()
    {
        // Create raycast & check collisions with interactables
        this.checkInteraction();
        this.CheckHit(WeaponManager._currentWeapon.GetWeaponRange());
        if (_player._instantiateWeapon)
        {
            _player._instantiateWeapon = false;
            // TO apo kato mporei na ginei methodos pou kaleitai apo to player animation

            //Kane to hdh yparxon _weapon tou trapezioy paidi kai metakinise to 
            //! den exoume ksekatharisei ean to weapon to spawn einai teli

            /*            
                        _player._currentWeapon = PlayerWeapon.SWORD;
                        _player._weapon = GetComponent<Sword>();
                        _player._weapon.transform.SetParent(_weaponHolder.transform);
                        _player._weapon.transform.localPosition = Vector3.zero;
                        _player._weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        _player._weapon.transform.localScale = Vector3.one;*/
        }


    }
    public void InvokeEnemyHit()
    {
        StartCoroutine(Effects.Flash(target.GetComponentInChildren<SpriteRenderer>(), 0.5f));
        GetComponent<PlayerController>().PlayVFX(target.GetComponent<Transform>());
        GetComponent<PlayerController>().PlayEquipSound();

    }

}

