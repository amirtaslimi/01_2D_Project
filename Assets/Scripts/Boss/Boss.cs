using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    public bool _detectGroundEnemy = false;
    public bool _detectPlayerClose = false;
    private float _playerDistance = 20f;
    private Rigidbody2D _myRigidbody;
    [SerializeField]
    private Transform _playerTransform;
    private float walkSpeed = 5f;
    private float runSpeed = 10f;
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FlipEnemyFacing();
        DetectPlayer();
        DetectClosePlayer();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("ForeGround")){
            walkSpeed = - walkSpeed;
        }
    }
    // private void OnTriggerStay2D(Collider2D other) {
    //     if (other.CompareTag("Player")){
    //         Debug.Log("detecting player");
    //         _detectGroundEnemy = true;
    //     }
    //     else{
    //         _detectGroundEnemy = false;
    //     }
    // }


    public void Walk(){
        _myRigidbody.velocity = new Vector2(walkSpeed , 0f);
    }



    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2 ((Mathf.Sign(_myRigidbody.velocity.x)) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        if (_myRigidbody.velocity.x == 0f)  
        {
            if (_playerTransform.transform.position.x > transform.position.x + 1f ){
                transform.localScale = new Vector2 (1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
                                                  
            }
            else if (_playerTransform.transform.position.x < transform.position.x + 1f){
                transform.localScale = new Vector2 (-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);

            }
        }
    }


    public void Pursue(){
        //Debug.Log(_playerTransform.transform.position.x + "next" + _playerTransform.transform.localScale.y);
        if (_playerTransform.transform.position.x > transform.position.x + 1f ){
            _myRigidbody.velocity = new Vector2(runSpeed , 0f);
        }
        else if (_playerTransform.transform.position.x < transform.position.x + 1f){
            _myRigidbody.velocity = new Vector2(-runSpeed , 0f);
        }
    }


    void DetectPlayer(){
        RaycastHit2D hitRight = Physics2D.Linecast(transform.position, transform.position + Vector3.right * _playerDistance,LayerMask.GetMask("Player"));
        RaycastHit2D hitLeft = Physics2D.Linecast(transform.position, transform.position + Vector3.left * _playerDistance,LayerMask.GetMask("Player"));
        
        Debug.DrawRay(transform.position, transform.position + Vector3.left * _playerDistance, Color.green);

        if ((hitRight.collider != null && hitRight.collider.gameObject.CompareTag("Player")) ||
            (hitLeft.collider != null && hitLeft.collider.gameObject.CompareTag("Player")))
        {
            Debug.Log("hitRight.collider");
            _detectGroundEnemy = true;
            
        }
        else{
            _detectGroundEnemy = false;
        }
    }

    void DetectClosePlayer(){
        if (Vector2.Distance(_playerTransform.position, transform.position) < 2f){
            _detectPlayerClose = true;
        }
        else
            _detectPlayerClose = false;
    }
}
