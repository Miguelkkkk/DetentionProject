using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{

    public ParticleSystem dust;
    public GameObject activeWeapon;

    [Header("Input")]
    public PlayerInputReader input;

    [Header("Movement")]
    [SerializeField] private float _playerSpd = 5;
    [SerializeField] private float _playerRunSpd = 10;
    private Vector2 _playerDir;
    private Vector2 _lastDir = new Vector2(1,0);

    private Rigidbody2D _playerRigidBody;
    private Animator _playerAnimator;

    private const string _vertical = "Vertical";
    private const string _horizontal = "Horizontal";
    private const string _lastVertical = "LastVertical";
    private const string _lastHorizontal = "LastHorizontal";

    [SerializeField] private int _dodgeDistance;
    [SerializeField] private float _dodgeStaminaCost;
    [SerializeField] private float _dodgeDuration = 0.5f;
    private PlayerData loadedPlayer;

    private bool _isDodging = false;
    private bool _isRunning = false;

    [Header("Stamina")]
    [SerializeField] private PlayerStamina playerStamina; 

    #region events

    private void OnEnable()
    {
        input.MovementEvent += OnMovement;
        input.RunEvent += OnRun;
        input.RunCancelledEvent += OnRunCancelled;
        input.DodgeEvent += OnDodge;

    }

    private void OnDisable()
    {
        input.MovementEvent -= OnMovement;
        input.RunEvent -= OnRun;
        input.RunCancelledEvent -= OnRunCancelled;
        input.DodgeEvent -= OnDodge;
    }

    private void OnDodge()
    {
        if (!_isDodging)
        {
            if (playerStamina.GetCurrentStamina() >= _dodgeStaminaCost)
            {
                _playerAnimator.SetTrigger("Dodge");
                StartCoroutine(DodgeRoll());
                playerStamina.UpdateStamina(-25.0f);
            }
        }
    }

    private void OnMovement(Vector2 movement)
    {
        _playerDir = movement.normalized;

        if (_playerDir != Vector2.zero)
        {
            _lastDir = _playerDir;
        }
    }

    private void OnRun()
    {
        _isRunning = true;
    }

    private void OnRunCancelled()
    {
        _isRunning = false;
    }

    #endregion

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Flip();

        if (!_isDodging)
        {
            _playerRigidBody.MovePosition(_playerRigidBody.position + _playerDir * _playerSpd * Time.fixedDeltaTime);
            _playerAnimator.SetFloat(_horizontal, _playerDir.x);
            _playerAnimator.SetFloat(_vertical, _playerDir.y);     
        }
        Run();
        if (_playerDir != Vector2.zero)
            {
                _playerAnimator.SetFloat(_lastHorizontal, _playerDir.x);
                _playerAnimator.SetFloat(_lastVertical, _playerDir.y);
            }
    }

    #region functions

    private void Flip()
    {
        if (_playerDir.x > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        }
        else if (_playerDir.x < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }
    }

    private void Run()
    {
        if (_isRunning && playerStamina != null && 
            playerStamina.GetCurrentStamina() > 0 && !_isDodging && _playerDir != Vector2.zero)
        {
            _playerSpd = _playerRunSpd;
            CreateDust();
            playerStamina.UseStamina(); 
        }
        else
        {
            _playerSpd = 5;
            if (playerStamina != null && !_isRunning && !_isDodging)
            {
                playerStamina.StartRegen(); 
            }
        }
    }

    private IEnumerator DodgeRoll()
    {
        Vector2 dodgeDirection;

        if (_isDodging) yield break;

        activeWeapon.gameObject.SetActive(false);

        dodgeDirection = _playerDir;
        if (dodgeDirection == Vector2.zero) 
        {
            dodgeDirection = _lastDir;
            _playerDir = dodgeDirection;
        }
        else 
        {
            _lastDir = dodgeDirection;
        }
        _isDodging = true;

        input.DisablePlayerAction();

        CreateDust();

        _playerRigidBody.AddForce(dodgeDirection * _dodgeDistance, ForceMode2D.Impulse);

        yield return new WaitForSeconds(_dodgeDuration);

        _playerRigidBody.velocity = Vector2.zero;

        _playerDir = Vector2.zero;

        input.EnablePlayerAction();

        PlayerData loadedPlayer = SaveManager.LoadPlayerData();

        if (loadedPlayer != null)
        {
            if (loadedPlayer.hasTakenSword)
            {
                activeWeapon.gameObject.SetActive(true);
            }
        }
        _isDodging = false;
    }

    private void CreateDust() { 
        dust.Play();
    }


    #endregion
}
