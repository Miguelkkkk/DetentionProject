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

    [Header("Player")]
    [SerializeField] private Rigidbody2D _rigidbody;

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
  
    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _playerDir * _playerSpd * Time.fixedDeltaTime);
    }

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

}
