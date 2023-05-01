using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    // variables

    [SerializeField]
    private LayerMask foreGround;

    public State state;


        //components variables
        private Rigidbody2D _myRigidBody;
        private CapsuleCollider2D _myCapsuleCollider;
         BoxCollider2D _myBoxCollider;
        private SpriteRenderer _mySpriteRendererPlayer;

////////////////////////////////////////////////////////////////


        //input actions variables
        [SerializeField]
        private InputActionReference _inputJump, _inputDash, _inputMove;

////////////////////////////////////////////////////////////////


        //movement variables
        private Vector2 _moveInput;
        [SerializeField]
        private float _playerMoveSpeed, _playerMoveAcceleration, _playerMoveMaxSpeed;
        private float _playerCurrentSpeed =0;
////////////////////////////////////////////////////////////////

        //jump variables
        
        [SerializeField]
        private float _jumpPower = 20f;
        private bool _doubleJump = true;
////////////////////////////////////////////////////////////////

        //dash variables
        private bool _isDashing = false;
        private bool _canDash = true;
        [SerializeField]
        private float _dashPower = 5f;
        [SerializeField]

        private float _dashCooldown = .5f;
        [SerializeField]

        private float _dashTime = 0.2f;

////////////////////////////////////////////////////////////////










    private void OnEnable() {
       
        _inputJump.action.performed += Jump;
        _inputDash.action.performed += Dash;
    }
    

    private void OnDisable() {
        _inputJump.action.performed -= Jump;

        _inputDash.action.performed -= Dash;
        
    }











    void Start()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
        _myBoxCollider = GetComponent<BoxCollider2D>();
        _myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        _mySpriteRendererPlayer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
    }

     private void FixedUpdate() {
        
        HandleMovement();
        StateManager();


    }

    private void HandleMovement()
    {
        Run();
        Flip();


        
    }


    private void Flip()
    {
       // if (Mathf.Abs(_myRigidBody.velocity.x) > Mathf.Epsilon)
       // {
            //transform.localScale = new Vector2(Mathf.Sign(_myRigidBody.velocity.x)*Mathf.Abs(transform.localScale.x), transform.localScale.y);
       // }
                // if (Mathf.Abs(PlayerAim.angle) > 90)
        // {
        //     transform.localScale = new Vector2(-1*Mathf.Abs(transform.localScale.x), transform.localScale.y);
        // }
        // else
        //     transform.localScale = new Vector2(1*Mathf.Abs(transform.localScale.x), transform.localScale.y);
        if (_myRigidBody.velocity.x > 0){
            _mySpriteRendererPlayer.flipX = false;
        }
        else if (_myRigidBody.velocity.x < 0){
            _mySpriteRendererPlayer.flipX = true;
        }
    }



    //utilities
    private bool isGrounded(){
        return (_myBoxCollider.IsTouchingLayers(foreGround));
     }
    void Jump(InputAction.CallbackContext callbackContext)
    {   
        if (isGrounded() || _doubleJump) {
            _myRigidBody.velocity = new Vector2(_myRigidBody.velocity.x, _jumpPower);
            _doubleJump = !_doubleJump;
            Debug.Log("can jump");
        }
        if (!isGrounded() && _doubleJump)
            _doubleJump = false;

    }
    void Dash(InputAction.CallbackContext callbackContext){
        if (_canDash){
            StartCoroutine(Idash());
        }
    }
    IEnumerator Idash(){
        _canDash = false;

        _isDashing = true;

        float v = _myRigidBody.velocity.x;


        float dashTimeTemp = _dashTime;

        float dir = Mathf.Sign(_myRigidBody.velocity.x);
        while (dashTimeTemp > 0)
        {
        _myRigidBody.velocity = new Vector2(_myRigidBody.velocity.x + _dashPower* dir*0.1f, _myRigidBody.velocity.y);

            dashTimeTemp -= Time.deltaTime;
            yield return null;
        }
        
        //_playerCurrentSpeed += _dashPower;
        //yield return new WaitForSeconds(_dashTime);
        //playerCurrentSpeed = v;
        _isDashing = false;

        float cooldownTemp = _dashCooldown;
        while (cooldownTemp > 0)
        {
            cooldownTemp -= Time.deltaTime;
            yield return null;
        }

        


        _canDash = true;
    } 
    private void Run()
    {
        //TODO: fix the problem in fliping 
        //fixed
        if (_isDashing) return;
        _moveInput = _inputMove.action.ReadValue<Vector2>();
        Vector2 vec = _moveInput;
        if (Mathf.Abs(_moveInput.x) > 0 && Mathf.Abs(_myRigidBody.velocity.x) >0){
            _playerCurrentSpeed += _playerMoveAcceleration * Time.deltaTime;
            state = State.Run;
        }
        else{
            _playerCurrentSpeed = 1;
            Debug.Log("not run");
        }
        _playerCurrentSpeed = Mathf.Clamp(_playerCurrentSpeed, 0, _playerMoveMaxSpeed) ;
        
        _myRigidBody.velocity = new Vector2((_playerCurrentSpeed  * _moveInput.x *_playerMoveSpeed ), _myRigidBody.velocity.y);
    }

    private void StateManager()
    {
        if (_isDashing){
            state = State.Dash;
        }
        else if (isGrounded()){

            if (Mathf.Abs(_myRigidBody.velocity.x)>Mathf.Epsilon){
                state = State.Run;
            }
            else{
                state = State.Idle;
            }
        }
            
        else if (_myRigidBody.velocity.y > 0){
            state = State.Jump;
            }

        
    }
    

    public enum State
    {
        Idle,
        Run,
        Jump,
        Fall,
        Climb,
        Dash,
        Sit
    }

}







//input handling


// void OnJump(InputValue value){
//     Debug.Log("lkj");
// }
// void OnMove(InputValue value){
//     _moveInput = value.Get<Vector2>();
// }

// void OnDash(InputValue value){

// }




