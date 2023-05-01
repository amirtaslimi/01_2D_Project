using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlueWeaponProp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform _bullet;
    private SpriteRenderer _weaponSprite;


    private float shootTime = 0f;


    private Bullet _myBulletSC;
    private Animator _animator;
    private PlayerWeapon _playerWeapon;


    private void OnEnable() {
        _playerWeapon._inputShoot.action.performed += Shoot;
    }
    

    private void OnDisable() {
        _playerWeapon._inputShoot.action.performed -= Shoot;
    }

    private void Awake()
    {
        _playerWeapon = GetComponentInParent<PlayerWeapon>();

    }

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _myBulletSC = _bullet.GetComponent<Bullet>();
        _weaponSprite = GetComponent<SpriteRenderer>();

    }
    
    private void Update()
    {
        _playerWeapon.WeaponFlip(_weaponSprite);
    }


    private void Shoot(InputAction.CallbackContext callbackContext) {
        if (Time.time > shootTime)
        {
            shootTime = Time.time + 1 / _myBulletSC.fireRate;
            Transform bulletTR = Instantiate(_bullet, _playerWeapon._endPoint.transform.position ,Quaternion.identity);
            Vector3 ShootDir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
            bulletTR.GetComponent<Bullet>().Setup(ShootDir, _playerWeapon._endPoint.transform.position);
            _animator.SetTrigger("isShooting");
        }

    }
}
