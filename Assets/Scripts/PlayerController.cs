using Kryz.CharacterStats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
//using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class PlayerController : MonoBehaviour, IDataPresistance
{

    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpImpulse = 10f;
    public float airWalkSpeed = 3f;
    public bool isLadder;
    public bool isClimbing;
    private float vertical;
    public StatPanel statPanel;
    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damageable damageable;
    public int key_amount = 0;
    public GameObject[] respawnPoints;
    public int respawnPointNum = -1;
    public GameObject startSpawn;

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        return airWalkSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

    }

    private bool _isMoving = false;

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    Rigidbody2D rb;
    Animator animator;

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if (!damageable.LockVelocity) 
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        }

        if (!damageable.LockVelocity && isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, CurrentMoveSpeed * moveInput.y);
        }
        else
        {
            rb.gravityScale = 3f;
        }

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    private void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        if (isLadder) // && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
        else
        {
            isClimbing = false;
        }
        OnDeath();
    }

    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x * airWalkSpeed, jumpImpulse);
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 15 || collision.gameObject.layer == 17)
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 15 || collision.gameObject.layer == 17)
        {
            isLadder = false;
            isClimbing = false;
        }
    }



    public void LoadData(GameData data)
    {
        ChangeStats();
        this.transform.position = data.PlayerPosition; 

    }


    public void SaveData(ref GameData data)
    { 
        if(this == null)
        {
            return;
        }
        data.PlayerPosition = this.transform.position;
    }


    public void ChangeStats()
    {
          CharacterStat[] stats = statPanel.GetStats();
        walkSpeed = 5 + ((int)stats[1].Value);
    }


    public void OnDeath()
    {
      
        if (damageable.Health <= 0)
        {            
            StartCoroutine(changeAnim());
        }
    }

    IEnumerator changeAnim()
    {
        if (true)
        {
            animator.SetBool("isAlive", false);
            yield return new WaitForSeconds(2f);
        }
        if (respawnPointNum >= 0)
        {
            transform.position = respawnPoints[respawnPointNum].transform.position;
            Debug.Log("deadge");
            damageable.Health = damageable.MaxHealth;
            yield return new WaitForSeconds(1.2f);

         
        }
        else
        {
            transform.position = startSpawn.transform.position;
            Debug.Log("deadge");
            damageable.Health = damageable.MaxHealth;
            yield return new WaitForSeconds(1.2f);

            
        }
        
        damageable.IsAlive = true;
        animator.SetBool("isAlive", true);

    }
}
