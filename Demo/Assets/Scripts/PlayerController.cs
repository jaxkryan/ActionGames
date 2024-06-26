using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpImpulse = 10f;
    public float airWalkSpeed = 3f;
    TouchingDirection touchingDirection;
    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }
    public float CurrentSpeed
    {
        get
        {
            if (canMove)
            {
                if (IsMoving && !touchingDirection.IsOnWall)
                {
                    if (touchingDirection.IsGround)
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
                //idle
                else
                    return 0;
            }
            //cant move
            else return 0;
        }
    }

    Vector2 moveInput;
    [SerializeField]
    private Boolean _isMoving = false;
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
    [SerializeField]
    private Boolean _isRunning = false;

    private Boolean _isFacingRight = true;

    public bool isFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentSpeed, rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            isFacingRight = false;
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

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attacking);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //TODO: cant jump when died
        if (context.started && touchingDirection.IsGround && canMove)
        {
            animator.SetTrigger(AnimationStrings.jumping);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    [Header("Dash properties")]
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCd = 1f;
    [SerializeField] private TrailRenderer trailRenderer;

    private IEnumerator Dash()
    {   
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        // Determine dash direction
        float dashDirection = 0f;
        if (moveInput.x != 0)
        {
            dashDirection = moveInput.x;
        }
        else
        {
            dashDirection = isFacingRight ? -1 : 1; // Dash backward if no input
        }

        rb.velocity = new Vector2(dashDirection * dashingPower, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        IsRunning = true; // Automatically enter run mode after dashing
        yield return new WaitForSeconds(dashingCd);
        canDash = true;
    }
}
