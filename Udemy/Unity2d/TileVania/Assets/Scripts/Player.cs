﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpVelocity = 5;
    [SerializeField] private float climbVelocity = 1;
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] private Collider2D feetCollider;
    [SerializeField] private Vector2 deathKick;

    public bool isAlive = true;

    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private float initialGravityScale;

    private enum Facing { left, right }        
    
	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        initialGravityScale = myRigidbody.gravityScale;
    }

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update () {
        Run();
        Jump();
        ClimbLadder();
        Death();
    }

    void Run()
    {
        if (!isAlive)
            return;

        float inputValue = CrossPlatformInputManager.GetAxis("Horizontal");

        bool isRunning = Mathf.Abs(inputValue) > Mathf.Epsilon;
        if (isRunning)
        {         
            bool isGoingRight = inputValue > Mathf.Epsilon;
            if (isGoingRight)
                mySpriteRenderer.flipX = false;    
            else   
                mySpriteRenderer.flipX = true;
        }

        myAnimator.SetBool("Running", isRunning);

        Vector2 newVelocity = new Vector2(runSpeed * inputValue * Time.deltaTime, myRigidbody.velocity.y);
        myRigidbody.velocity = newVelocity;

    }

    private void Jump()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || !isAlive)
        {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jump = new Vector2(0, jumpVelocity);
            myRigidbody.velocity += jump;
        }
    } 

    private void ClimbLadder()
    {
        if (!bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")) || !isAlive) {
            myAnimator.SetBool("Climbing", false);
            myAnimator.SetBool("ClimbingIdle", false);
            myRigidbody.gravityScale = initialGravityScale;
            return; }

        float inputValue = CrossPlatformInputManager.GetAxis("Vertical");

        bool inLadder = Mathf.Abs(inputValue) > Mathf.Epsilon;
        myRigidbody.gravityScale = 0;
        if (inLadder)
        {
            myAnimator.SetBool("ClimbingIdle", true);
            myAnimator.SetBool("Climbing", true); 
        }
        else
        {                                        
            myAnimator.SetBool("Climbing", false);
            if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
                myAnimator.SetBool("ClimbingIdle", false);
            }
        }


        Vector2 newVelocity = new Vector2(myRigidbody.velocity.x, climbVelocity * inputValue);
        myRigidbody.velocity = newVelocity;

    }

    void Death()
    {    
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")) && isAlive){
            isAlive = false;
            myAnimator.SetTrigger("Death");            
            myRigidbody.velocity = deathKick;
            StartCoroutine(ProcessDeath());
        }
    }

    IEnumerator ProcessDeath()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<GameManager>().OnPlayerDeath();
    }

}
