using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform healthBar;
    [SerializeField] float moveSpeed = 20f;
    SpriteRenderer enemySprite ;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    private float _maxHealth = 50f;
    private float _health = 50f; 
    private bool _isAlive = true;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        enemySprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2 (moveSpeed, 0f);
        Alive();
    }

    private void Alive()
    {
        if (_health <=0){
            myAnimator.SetBool("isDead", true);
        }
        setHealthBar();
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("ForeGround")){
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Bullet")){
            if (!_isAlive) {return;}
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            _health -= bullet.bulletDamage;
            _health = Mathf.Clamp(_health, 0, _maxHealth);
            Destroy(other.gameObject);
            myAnimator.SetTrigger("isHurt");
            
        }
    }

    void FlipEnemyFacing()
    {

        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidbody.velocity.x)) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }


    void EnemyDeath(){


        moveSpeed = 0f;
        Destroy(gameObject, 1f);

    }


    IEnumerator WaitForSlowdown()
{
    yield return new WaitForSeconds(3);
}


    void setHealthBar(){
        float size = _health/_maxHealth;
        healthBar.transform.localScale = new Vector2(size, healthBar.transform.localScale.y);
    }
}
