using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    private CapsuleCollider2D _touchingCol;
    public ContactFilter2D _castFilter;
    private Animator _animator;

    public float groundDistance = 0.05f;
    public float wallDistance = 0.5f;
    public float ceilingDistance = 0.05f;


    private RaycastHit2D[] _groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] _wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] _ceilingHits = new RaycastHit2D[5];

    public bool _isGrounded = true;
    public bool _isOnWall;
    //public bool _isOnCeiling;

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsGrounded { get { return _isGrounded; } private set 
        { 
            _isGrounded = value;
            _animator.SetBool(AnimationStrings.isGrounded, value);
        } 
    }
    public bool IsOnWall
    {
        get { return _isOnWall; }
        private set
        {
            _isOnWall = value;
            _animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }
    //public bool IsOnCeiling
    //{
    //    get { return _isOnCeiling; }
    //    private set
    //    {
    //        _isOnCeiling = value;
    //        _animator.SetBool(AnimationStrings.isOnCeiling, value);
    //    }
    //}

    private void Awake()
    {
        _touchingCol = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        IsGrounded = _touchingCol.Cast(Vector2.down, _castFilter, _groundHits, groundDistance) > 0;
        IsOnWall = _touchingCol.Cast(wallCheckDirection, _castFilter, _wallHits, wallDistance) > 0;
        //IsOnCeiling = _touchingCol.Cast(Vector2.up, _castFilter, _ceilingHits, ceilingDistance) > 0;
    }

}
