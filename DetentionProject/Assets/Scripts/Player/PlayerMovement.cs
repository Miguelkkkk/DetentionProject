using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input")]
    public InputReader input;

    [Header("Movement")]
    [SerializeField] private float _playerSpd = 5;
    private Vector2 _playerDir;

    [Header("Camera")]
    public Camera _camera;

    private Rigidbody2D _playerRigidBody;
    private Animator _playerAnimator;

    private const string _vertical = "Vertical";
    private const string _horizontal = "Horizontal";
    private const string _lastVertical = "LastVertical";
    private const string _lastHorizontal = "LastHorizontal";

    #region events

    private void OnEnable()
    {
        input.MovementEvent += OnMovement;
    }

    private void OnDisable()
    {
        input.MovementEvent -= OnMovement;
    }
    private void OnMovement(Vector2 movement)
    {
        _playerDir = movement.normalized;
    }
    #endregion

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (_playerDir != Vector2.zero)
        {
            RotatePlayer(true);
        }
        else {
            RotatePlayer(false);
        }
        _playerRigidBody.MovePosition(_playerRigidBody.position + _playerDir * _playerSpd * Time.fixedDeltaTime);

    }

    #region functions

    private void RotatePlayer(bool isMoving) {

        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDir;
        mouseDir = mousePosition - transform.position;
        mouseDir.Normalize(); 
        Flip(mouseDir);
        if (isMoving)
        {
            _playerAnimator.SetFloat(_horizontal, mouseDir.x);
            _playerAnimator.SetFloat(_vertical, mouseDir.y);
        }
        else
        {
            _playerAnimator.SetFloat(_horizontal, 0);
            _playerAnimator.SetFloat(_vertical, 0);
            _playerAnimator.SetFloat(_lastHorizontal, mouseDir.x);
            _playerAnimator.SetFloat(_lastVertical, mouseDir.y);
        }
    }
    void Flip(Vector2 direction)
    {
        if (direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        }
        else if (direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }
    }
    #endregion 
}