using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private Rigidbody2D _rb;
    public DetectionZone _attackZone;
    private Animator _animator;
    private Damageable _damageable;

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            _animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        HasTarget = _attackZone._detectedColliders.Count > 0;
    }

    //public void OnAttack()
    //{
    //    if(_hasTarget)
    //    {
            
    //    }
    //}

}
