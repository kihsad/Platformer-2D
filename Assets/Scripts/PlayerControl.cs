using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerControl : MonoBehaviour
{
    public float walkSpeed = 5f;
    Vector2 _moveInput;
    public float jumpImpulse = 5f;
    private bool _isMoving = false;
    private TouchingDirections _touchingDirections;

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
    }

    private void FixedUpdate()
    {
        //TODO 
        //if (!_touchingDirections.IsOnWall)
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
        if(context.started && _touchingDirections.IsGrounded)
        {
            _animator.SetTrigger(AnimationStrings.jumpTrigger);
            _rb.velocity = new Vector2(_rb.velocity.x, jumpImpulse);
        }
    }
    public void OnAttack(InputAction.CallbackContext contex)
    {
        if(contex.started)
        {
            _animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }
}
