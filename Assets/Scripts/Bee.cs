using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public float flightSpeed = 2f;
    public float waypointReachedDistance = 0.1f;
    public DetectionZone _detectionZone;
    public List<Transform> _waypoints;

    private Animator _animator;
    private Rigidbody2D _rb;
    private Damageable _damageable;

    public bool _hasTarget = false;

    private Transform _nextWaypoint;
    public int _waypointNum;
    

    public bool HasTarget
    {
        get { return _hasTarget; }
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
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        _nextWaypoint = _waypoints[_waypointNum];
    }

    private void Update()
    {
        HasTarget = _detectionZone._detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if(_damageable.IsAlive)
        {
            if(CanMove)
            {
                Flight();
            }
            else
            {
                _rb.velocity = Vector3.zero;
            }
        }
        else
        {
            _rb.gravityScale = 2f;
        }
    }

    private void Flight()
    {
        Vector2 directionToWaypoint = (_nextWaypoint.position-transform.position).normalized;
        float distance = Vector2.Distance(_nextWaypoint.position, transform.position);
        _rb.velocity = directionToWaypoint * flightSpeed;

        if(distance <= waypointReachedDistance)
        {
            _waypointNum++;

            if(_waypointNum >= _waypoints.Count)
            {
                _waypointNum = 0;
            }
            _nextWaypoint = _waypoints[_waypointNum];
        }
    }
}
