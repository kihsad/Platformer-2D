using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerControl : MonoBehaviour
{
    public float walkSpeed = 5f;
    public GameObject projectilePrefab;

    Vector2 _moveInput;
    public float jumpImpulse = 8f;
    private bool _isMoving = false;
    private TouchingDirections _touchingDirections;
    private Damageable _damageable;
    private int _currentJumps;
    private int maxJumps = 2;
    private float doublejumpImpulse = 4f;
    private ProjectileLauncher _projectileLauncher;

    public bool IsMoving { get 
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            _animator.SetBool(AnimationStrings.isMoving, value);
        }
        }
    public bool IsFacingRight { get { return _isFacingRight; } private set
        {
            if(_isFacingRight != value)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, 1f, 1f);
            }
            _isFacingRight = value;
        }
        }

    Rigidbody2D _rb;
    Animator _animator;
    private bool _isFacingRight = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchingDirections = GetComponent<TouchingDirections>();
        _damageable = GetComponent<Damageable>();
        _currentJumps = 1;
        _projectileLauncher = GetComponent<ProjectileLauncher>();
    }

    private void FixedUpdate()
    {
        //TODO  
        //if (!_touchingDirections.IsOnWall)
        //if(!_damageable.IsHit)
        _rb.velocity = new Vector2(_moveInput.x * walkSpeed, _rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        IsMoving = _moveInput != Vector2.zero;
        SetFacingDirection(_moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(_moveInput.x>0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (_moveInput.x<0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && _touchingDirections.IsGrounded)
        {
            _animator.SetTrigger(AnimationStrings.jumpTrigger);
            _rb.velocity = new Vector2(_rb.velocity.x, jumpImpulse);
            _currentJumps++;
            Debug.Log("1");
        }
        else if (context.performed && _currentJumps < maxJumps)
        {
            _animator.SetTrigger(AnimationStrings.jumpTrigger);
            _rb.velocity = new Vector2(_rb.velocity.x, jumpImpulse + doublejumpImpulse);
            _currentJumps++;
            Debug.Log("2");
        }
        //Debug.Log(_currentJumps);

        if (_touchingDirections.IsGrounded) _currentJumps = 1;
        if (_currentJumps >= maxJumps) return;
        
    }
    public void OnAttack(InputAction.CallbackContext contex)
    {
        if(contex.started)
        {
            _animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext contex)
    {
        if (contex.started)
        {
            //_projectileLauncher.FireProjectile();
            GameObject projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            Vector3 origScale = projectile.transform.localScale;
            projectile.transform.localScale = new Vector3(
                origScale.x * transform.localScale.x > 0 ? -1 : 1,
                origScale.y,
                origScale.z
                );
            //_animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
        }
    }
}
