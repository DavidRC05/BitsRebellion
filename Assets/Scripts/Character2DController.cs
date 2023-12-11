using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Character2DController : MonoState<Character2DController>
{
    [Header("Move")]
    [SerializeField]
    float moveSpeed = 300.0F;

    [SerializeField]
    bool isFacingRight = true;

    [Header("Jump")]
    [SerializeField]
    float jumpForce = 800.0F;

    [SerializeField]
    float fallMultiplier = 3.0F;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    float jumpGraceTime = 0.20F;

    [Header("Combat")]
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform[] firePoints;

    [SerializeField]
    [Range(0.1F, 1.0F)]
    float fireRate = 0.3F;

    [SerializeField]
    LayerMask enemyMask;

    [Header("Animation")]
    [SerializeField]
    Animator animator;

    //ARTHURO
    [Header("Enemy Rebound")]
    [SerializeField]
    float reboundY = 5.0F;

    Rigidbody2D _rb;

    Vector2 _direction;


    bool _isMoving;
    bool _isJumping;
    bool _isJumpPressed;


    float _gravityY;
    float _lasTimeJumpPressed;

    float _fireTimer;

    //Arthuro
    [HideInInspector]
    public bool _canMove = true;

    protected override void Awake()
    {
        base.Awake();

        _rb = GetComponent<Rigidbody2D>();
        _gravityY = -Physics2D.gravity.y;
    }


    void Update()
    {
        HandleInputs();

        //Shooting
        _fireTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && _fireTimer <= 0.0F)
        {
            Shoot();
            _fireTimer = fireRate;
        }
    }

    void FixedUpdate()
    {

        HandleJump();
        HandleFlipX();
        HandleMove();

    }

    void HandleInputs()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0.0F);
        _isMoving = _direction.x != 0.0F;

        _isJumpPressed = Input.GetButtonDown("Jump");

        if (_isJumpPressed)
        {
            _lasTimeJumpPressed = Time.time;
        }
    }

    void HandleMove()
    {
        if (!_canMove)
        {
            return;
        }

        bool isMoving = animator.GetFloat("speed") > 0.01F;

        if (_isMoving != isMoving && !_isJumping)
        {
            animator.SetFloat("speed", Mathf.Abs(_direction.x));
        }

        Vector2 velocity = _direction * moveSpeed * Time.fixedDeltaTime;
        velocity.y = _rb.velocity.y;
        _rb.velocity = velocity;
    }

    void HandleFlipX()
    {
        if (!_isMoving)
        {
            return;
        }

        bool facingRight = _direction.x > 0.0F;
        if (isFacingRight != facingRight)
        {
            isFacingRight = facingRight;
            transform.Rotate(0.0F, 180.0F, 0.0F);
        }
    }

    void HandleJump()
    {
        if (_lasTimeJumpPressed > 0.0F && Time.time - _lasTimeJumpPressed <= jumpGraceTime)
        {
            _isJumpPressed = true;
        }
        else
        {
            _lasTimeJumpPressed = 0.0F;
        }

        if (_isJumpPressed)
        {
            bool isGrounded = IsGrounded();
            if (isGrounded)
            {
                _rb.velocity += Vector2.up * jumpForce * Time.fixedDeltaTime;
            }
        }

        if (_rb.velocity.y < 0) // Only apply fallMultiplier when the player is actually falling
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }


        _isJumping = !IsGrounded();

        bool isNegativeVelocityY = _rb.velocity.y < -0.1F;

        /*  Este codigo nos puede servir si agregamos animaciones para saltar y caer
           
         bool isJumping = animator.GetBool("isJumping");
        if (_isJumping != isJumping)
        {
            animator.SetBool("isJumping", _isJumping);
        }

        bool isFalling = animator.GetBool("isFalling");
        bool isNegativeVelocityY = _rb.velocity.y < -0.01F;
        if (isNegativeVelocityY != isFalling)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", isNegativeVelocityY);
        }
        */

    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(
            groundCheck.position, new Vector2(0.50F, 0.06F),
            CapsuleDirection2D.Horizontal, 0.0F, groundMask);
    }


    void Shoot()
    {
        foreach (Transform firePoint in firePoints)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    //Arthuro
    public void Rebound()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, reboundY);
    }


}