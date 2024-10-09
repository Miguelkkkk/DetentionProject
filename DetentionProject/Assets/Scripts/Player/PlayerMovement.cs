using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input")]
    public InputReader input;

    [Header("Movement")]
    [SerializeField] private float _playerSpd = 5;
    [SerializeField] private float _playerRunSpd = 10;
    private Vector2 _playerDir;

    [Header("Camera")]
    public Camera _camera;

    private Rigidbody2D _playerRigidBody;
    private Animator _playerAnimator;

    private const string _vertical = "Vertical";
    private const string _horizontal = "Horizontal";
    private const string _lastVertical = "LastVertical";
    private const string _lastHorizontal = "LastHorizontal";

    private bool _isRunning = false;

    #region events

    private void OnEnable()
    {
        input.MovementEvent += OnMovement;
        input.RunEvent += OnRun;
        input.RunCancelledEvent += OnRunCancelled;
    }

    private void OnDisable()
    {
        input.MovementEvent -= OnMovement;
        input.RunEvent -= OnRun;
        input.RunCancelledEvent -= OnRunCancelled;
    }
    private void OnMovement(Vector2 movement)
    {
        _playerDir = movement.normalized;
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
    void FixedUpdate()
    {
        Flip();
        _playerRigidBody.MovePosition(_playerRigidBody.position + _playerDir * _playerSpd * Time.fixedDeltaTime);
        _playerAnimator.SetFloat(_horizontal, _playerDir.x);
        _playerAnimator.SetFloat(_vertical, _playerDir.y);

        if (_playerDir != Vector2.zero)
        {
            _playerAnimator.SetFloat(_lastHorizontal, _playerDir.x);
            _playerAnimator.SetFloat(_lastVertical, _playerDir.y);
        }

        Run();

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
        if (_isRunning == true)
        {
            _playerSpd = _playerRunSpd;
        }
        else { 
            _playerSpd = 5; 
        }
    }
    #endregion 
}

