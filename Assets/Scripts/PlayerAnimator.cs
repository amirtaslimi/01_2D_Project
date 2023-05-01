using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerAnimator : MonoBehaviour
{
    PlayerMovement playerMovement;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        myAnimator = GetComponent<Animator>();
        PlayAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        PlayAnimation();
    }
    private void PlayAnimation()
    {
        if (playerMovement.state == PlayerMovement.State.Idle){
            myAnimator.SetBool("isIdle", true);
        }
        else {
            myAnimator.SetBool("isIdle", false);
        }
        if (playerMovement.state == PlayerMovement.State.Dash){
            myAnimator.SetBool("isDashing", true);
        }
        else {
            myAnimator.SetBool("isDashing", false);
        }
        if (playerMovement.state == PlayerMovement.State.Run){
            myAnimator.SetBool("isRunning", true);
        }
        else {
            myAnimator.SetBool("isRunning", false);
        }
        if (playerMovement.state == PlayerMovement.State.Jump){
            myAnimator.SetBool("isJumping", true);
        }
        else {
            myAnimator.SetBool("isJumping", false);
        }
        if (playerMovement.state == PlayerMovement.State.Fall){
            myAnimator.SetBool("isFalling", true);
        }
        else {
            myAnimator.SetBool("isFalling", false);
        }
        if (playerMovement.state == PlayerMovement.State.Climb){
            myAnimator.SetBool("isClimbing", true);
        }
        else {
            myAnimator.SetBool("isClimbing", false);
        }
        if (playerMovement.state == PlayerMovement.State.Sit){
            myAnimator.SetBool("isSitting", true);
        }
        else {
            myAnimator.SetBool("isSitting", false);
        }
    

    }
    
}

