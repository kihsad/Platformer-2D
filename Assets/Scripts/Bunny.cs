using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class Bunny : MonoBehaviour
{
    public float walkSpeed = 3f;
    private float walkStopRate = 0.02f;
    private Rigidbody2D _rb;
    private TouchingDirections _touchingDirections;
    public DetectionZone _attackZone;
    private Animator _animator;
    public enum WalkableDirection { Right, Left}

    private WalkableDirection _walkDirection;
    private Vector2 _walkDirectionVector;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set { 
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if(value == WalkableDirection.Right)
                {
                    _walkDirectionVector = Vector2.right;
                }
                else if(value == WalkableDirection.Left)
                {
                    _walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value; }
    }

    public bool _hasTarget = false;

    public bool HasTarget { get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            _animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return _animator.GetBool(AnimationStrings.canMove);
        }
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _touchingDirections = GetComponent<TouchingDirections>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HasTarget = _attackZone._detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if(/*_touchingDirections.IsGrounded &&*/ _touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if (CanMove)
            _rb.velocity = new Vector2(walkSpeed * _walkDirectionVector.x, _rb.velocity.y);
        else
            _rb.velocity = new Vector2(Mathf.Lerp(_rb.velocity.x, 0, walkStopRate), _rb.velocity.y);
    }

    private void FlipDirection()
    {
        if(_walkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else
        {
            WalkDirection = WalkableDirection.Right;
        }
    }

}
