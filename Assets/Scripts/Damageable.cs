using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private Animator _animator;


    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if(_health <= 0)
            {
                IsAlive = false;
            }
        }
    }
    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;

    public bool IsHit
    {
        get
        {
            return _animator.GetBool(AnimationStrings.isHit);
        }
        private set
        {
            _animator.SetBool(AnimationStrings.isHit, value);
        }
    }

    private float timeSinceHit = 0;
    private float invincibilityTime = 0.25f;

    public bool IsAlive 
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            _animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set" + value);
        }
    }
    private void Awake()
    {
        
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    public void Hit(int damage)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            //IsHit = true;
            _animator.SetTrigger(AnimationStrings.hitTrigger);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
        }
    }
}
