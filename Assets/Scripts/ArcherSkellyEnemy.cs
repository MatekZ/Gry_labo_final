using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class ArcherSkellyEnemy : MonoBehaviour
{
    public float walkAcceleration = 3f;
    public float walkStopRate = 0.05f;
    public float maxSpeed = 3f;
    //public DetectionZone attackZone;
    public DetectionZone rangedAttackZone;
    public DetectionZone cliffDetectionZone;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;
    public ProjectileLauncher projectileLauncher;


    public enum WalkableDirections { Right, Left }

    private WalkableDirections _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirections WalkDirection
    {
        get
        {
            return _walkDirection;
        }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirections.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirections.Left)
                {
                    walkDirectionVector = Vector2.left;

                }
            }
            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }


    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        set
        {

            animator.SetFloat(AnimationStrings.attackCooldown, MathF.Max(value, 0));
        }
    }

    public float RangedAttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.rangedAttackCooldown);
        }
        set
        {

            animator.SetFloat(AnimationStrings.rangedAttackCooldown, MathF.Max(value, 0));
        }
    }


    IEnumerator WaitAndShoot()
    {
        yield return new WaitForSeconds(2f);
        projectileLauncher.FireProjectile();
    }

    public void StartShooting()
    {
        StartCoroutine(WaitAndShoot());
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        /*HasTarget = attackZone.detectedColliders.Count > 0;


        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }*/

        HasTarget = rangedAttackZone.detectedColliders.Count > 0;

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        if (!damageable.LockVelocity)
        {
            if (CanMove && touchingDirections.IsGrounded)
            {
                rb.velocity = new Vector2(
                    Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.deltaTime), -maxSpeed, maxSpeed), rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirections.Right)
        {
            WalkDirection = WalkableDirections.Left;
        }
        else if (WalkDirection == WalkableDirections.Left)
        {
            WalkDirection = WalkableDirections.Right;
        }
        else
        {
            Debug.Log("Wrong value of direction!");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}
