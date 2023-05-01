using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float bulletSpeed = 20f;
    [SerializeField] public float fireRate = 10f;
    [SerializeField] public float bulletDamage = 12f;
    [SerializeField] private float bulletDestroyTime = 1f;
    public Vector3 shootDir;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.position += shootDir * (Time.fixedDeltaTime * bulletSpeed);
    }

    public void Setup(Vector3 shootDir, Vector3 endPointPosition){
            //Transform bulletTR = Instantiate(_bullet, endPointPosition ,Quaternion.identity);

            this.shootDir = shootDir;
            transform.rotation = Quaternion.AngleAxis(PlayerAim.angle, Vector3.forward);
            Destroy(gameObject, bulletDestroyTime);
    }

    void bulletDestory(){
        Destroy(gameObject);
    }
}
