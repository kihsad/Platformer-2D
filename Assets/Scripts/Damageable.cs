using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private PlayerControl _control;
    private Animator _animator;
    private Block _block;
    public int _enemyXp;

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
                _control.GainXP(_enemyXp);
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
        _control = FindObjectOfType<PlayerControl>();
        _animator = GetComponent<Animator>();
        _block = GetComponent<Block>();
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
        if (_block == null)
        {
            if (IsAlive && !isInvincible)
            {
                Health -= damage;
                isInvincible = true;
                //IsHit = true;
                _animator.SetTrigger(AnimationStrings.hitTrigger);
                CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            }
        }
        else if (!_block.isActive)
        {
            if (IsAlive && !isInvincible)
            {
                Health -= damage;
                isInvincible = true;
                //IsHit = true;
                _animator.SetTrigger(AnimationStrings.hitTrigger);
                CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            }
        }
    }

    public void Heal(int healthRestore)
    {
        if(IsAlive)
        {
            Health += healthRestore;
            CharacterEvents.characterHealed(gameObject, healthRestore);
        }
    }
}
