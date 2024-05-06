using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input")]
    public InputReader Input;

    [Header("Movement")]
    [SerializeField] private float _playerSpd = 5;
    private Vector2 _playerDir;

    private Rigidbody2D _playerRigidBody;
    private Animator _playerAnimator;

    private const string _vertical = "Vertical";
    private const string _horizontal = "Horizontal";



    #region events

    private void OnEnable()
    {
        Input.MovementEvent += OnMovement;
    }

    private void OnDisable()
    {
        Input.MovementEvent -= OnMovement;
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
        Flip();
        _playerRigidBody.MovePosition(_playerRigidBody.position + _playerDir * _playerSpd * Time.fixedDeltaTime);
        _playerAnimator.SetFloat(_horizontal, _playerDir.x);
        _playerAnimator.SetFloat(_vertical, _playerDir.y);
    }

    #region functions
    void Flip()
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
    #endregion 

}
