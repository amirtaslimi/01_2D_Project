using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour

{

    [SerializeField] public Transform _endPoint;
    private Animator _animator;
    private int SelectedWeapon;

    [SerializeField] private Transform _weaponsList;


    [SerializeField] public InputActionReference _inputShoot, _inputScrolWheel;


    private void OnEnable()
    {

        _inputScrolWheel.action.performed += SwitchWeapon;
    }


    private void OnDisable()
    {

        _inputScrolWheel.action.performed -= SwitchWeapon;

    }

    void Start()
    {
        
        _animator = GetComponentInChildren<Animator>();
    }
    
    
    private void SwitchWeapon(InputAction.CallbackContext callbackContext)
    {
        int temp = SelectedWeapon;
        Debug.Log("scrol" + callbackContext.ReadValue<float>());
        if (callbackContext.ReadValue<float>() > 0)
        {
            if (SelectedWeapon >= transform.childCount )
            {
                SelectedWeapon = 0;
            }
            else
            {
                SelectedWeapon++;
            }
        }
        
        else if(callbackContext.ReadValue<float>() < 0)
        {
            if (SelectedWeapon <= 0)
            {
                SelectedWeapon = transform.childCount;      
            }
            else
            {
                SelectedWeapon--;
            }
        }
        
        if(temp != SelectedWeapon)
            selecteWeapon();
}

private void selecteWeapon()
    {
        var i = 0;//int
        foreach (Transform weapons in _weaponsList.transform)
        {
            if (i == SelectedWeapon)
            {
                weapons.gameObject.SetActive(true);
            }
            else
            {
                weapons.gameObject.SetActive(false);
            }

            i++;
        }
    }

public void WeaponFlip(SpriteRenderer _mySpriteRendererWeapon)
{
    if (Mathf.Abs(PlayerAim.angle) > 90 || Mathf.Abs(PlayerAim.angle) < -90)
    {
        //transform.localScale = new Vector2(transform.localScale.x, -1*Mathf.Abs(transform.localScale.y));
        _mySpriteRendererWeapon.flipY = true;
    }
    else
        //transform.localScale = new Vector2(transform.localScale.x, 1*Mathf.Abs(transform.localScale.y));
        _mySpriteRendererWeapon.flipY = false;
}

}
