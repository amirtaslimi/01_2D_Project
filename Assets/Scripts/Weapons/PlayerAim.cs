using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer _mySpriteRendererWeapon;
    public Transform aimPosition ;
    public static float angle =0;
    public static Vector3 shootDir;
    void Start()
    {   
        _mySpriteRendererWeapon = GetComponentInChildren<SpriteRenderer>();

        
    }

    // Update is called once per frame
    private void Update()
    {
        GetMousePosition();
        WeaponFlip();
    }

    private void WeaponFlip()
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

    void GetMousePosition(){
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - aimPosition .position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        aimPosition.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //Debug.Log(angle);
    }
}
