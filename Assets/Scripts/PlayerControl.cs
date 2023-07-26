using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private Stat _xp;
    [SerializeField]
    private float _xpValue;
    [SerializeField]
    private float _maxXp;
    [SerializeField]
    private int _level;
    [SerializeField]
    private Text _levelText;
    [SerializeField]
    private GameObject _abilityPanel;

    public AudioClip movement;

    public float walkSpeed = 5f;
    public GameObject projectilePrefab;

    private Vector2 _moveInput;
    public float jumpImpulse = 8f;
    private bool _isMoving = false;
    private TouchingDirections _touchingDirections;
    private Damageable _damageable;
    private int _currentJumps;
    private int maxJumps = 2;
    private float doublejumpImpulse = 4f;
    private ProjectileLauncher _projectileLauncher;
    private float _timer;

    public float dashForce;
    private int _damage;

    private bool canDash;
    private bool canUseAbilities;
   

    private static PlayerControl instance;
    public static PlayerControl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerControl>();
            }
            return instance;
        }
    }
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            _animator.SetBool(AnimationStrings.isMoving, value);
        }
    }
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, 1f, 1f);
            }
            _isFacingRight = value;
        }
    }

    public int MyLevel
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
        }
    }

    Rigidbody2D _rb;
    Animator _animator;
    private Attack _attack;
    private bool _isFacingRight = true;
    private Block _block;
    private Transformation _transformation;
    public AudioClip dingSound;
    public AudioClip meleeAttackSound;
    public AudioClip rangedAttackSound;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchingDirections = GetComponent<TouchingDirections>();
        _damageable = GetComponent<Damageable>();
        _currentJumps = 1;
        _projectileLauncher = GetComponent<ProjectileLauncher>();
        _xp.Initialized(0, Mathf.Floor(100 * MyLevel * Mathf.Pow(MyLevel, 0.5f)));
        _levelText.text = MyLevel.ToString();
        _attack = GetComponentInChildren<Attack>();
        _block = GetComponent<Block>();
        _transformation = GetComponent<Transformation>();
        _damage = _attack.Damage;
        _abilityPanel.SetActive(false);
        _timer = 0;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_moveInput.x * walkSpeed, _rb.velocity.y);
        _timer += Time.deltaTime;
        if (_timer >= 0.3f && _touchingDirections.IsGrounded && IsMoving)
        {
            SoundManager.Instance.PlaySound(movement);
            _timer = 0;
        }
    }

    public void GainXP(int xp)
    {
        _xp.MyCurrentValue += xp;

        if (_xp.MyCurrentValue >= _xp.MyMaxValue)
        {
            StartCoroutine(Ding());
        }
    }

    private IEnumerator Ding()
    {
        while (!_xp.IsFull)
        {
            yield return null;
        }
        MyLevel++;
        _levelText.text = MyLevel.ToString();
        _xp.MyMaxValue = 100 * MyLevel * Mathf.Pow(MyLevel, 0.5f);
        _xp.MyMaxValue = Mathf.Floor(_xp.MyMaxValue);
        _xp.MyCurrentValue = _xp.MyOverFlow;
        _xp.Reset();
        SoundManager.Instance.PlaySound(dingSound);
        _abilityPanel.SetActive(true);
        canUseAbilities = true;
        yield return new WaitForSeconds(10);
        _abilityPanel.SetActive(false);
        canUseAbilities = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        IsMoving = _moveInput != Vector2.zero;
        SetFacingDirection(_moveInput);
        //if (_timer >= 0.2f)
        //{
        //    SoundManager.Instance.PlaySound(movement);
        //    _timer = 0;
        //}
    }

    public void SetFacingDirection(Vector2 moveInput)
    {
        if (this._moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (this._moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        canDash = true;
        if (context.performed && _touchingDirections.IsGrounded)
        {
            _animator.SetTrigger(AnimationStrings.jumpTrigger);
            _rb.velocity = new Vector2(_rb.velocity.x, jumpImpulse);
            _currentJumps++;
        }
        else if (context.performed && _currentJumps < maxJumps)
        {
            _animator.SetTrigger(AnimationStrings.jumpTrigger);
            _rb.velocity = new Vector2(_rb.velocity.x, jumpImpulse + doublejumpImpulse);
            _currentJumps++;
        }

        if (_touchingDirections.IsGrounded) _currentJumps = 1;
        if (_currentJumps >= maxJumps) return;
        canDash = false;
    }
    public void OnAttack(InputAction.CallbackContext contex)
    {
        if (contex.started)
        {
            _attack.Damage = _damage;
            SoundManager.Instance.PlaySound(meleeAttackSound);
            _animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext contex)
    {
        if (contex.started)
        {
            SoundManager.Instance.PlaySound(rangedAttackSound);
            GameObject projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            Vector3 origScale = projectile.transform.localScale;
            projectile.transform.localScale = new Vector3(
                origScale.x * transform.localScale.x > 0 ? -1 : 1,
                origScale.y,
                origScale.z
                );
        }
    }

    public void OnStrongAttack(InputAction.CallbackContext context)
    {   
        if (context.started && canUseAbilities)
        {
            _attack.Damage = _damage * 2;
            _animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnDash(/*InputAction.CallbackContext context*/)
    {
        if (/*context.started && *//*!_touchingDirections.IsGrounded*/ canDash && canUseAbilities)
        {
            Debug.Log("start dash");
            if (IsFacingRight)
            {
                _rb.AddForce(Vector2.right * dashForce);
                //float origGravity = _rb.gravityScale;
                //_rb.gravityScale = 0f;
                //_rb.velocity = Vector2.right * dashForce;
                //_rb.gravityScale = origGravity;
                Debug.Log("dash right");
            }
            else
            {
                _rb.AddForce(Vector2.left * dashForce);
                //float origGravity = _rb.gravityScale;
                //_rb.gravityScale = 0f;
                //_rb.velocity = Vector2.left * dashForce;
                //_rb.gravityScale = origGravity;
                Debug.Log("dash left");
            }

        }
    }
    public void OnChangeSkin(InputAction.CallbackContext context)
    {
        if (context.started && canUseAbilities)
        {
            _transformation.ChangeSkin();
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.started && canUseAbilities)
        {
            _block.SetActiveShield(_damageable);
        }
    }
}
