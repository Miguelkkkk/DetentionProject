using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerAttack : MonoBehaviour
{
    [Header("Input")]
    public InputReader input;

    [Header("Camera")]
    public Camera _camera;

    [Header("Attack Drag")]
    [SerializeField] private float _attackDrag = 5f;

    private Rigidbody2D _playerRigidBody;
    private Animator _playerAnimator;

    private const string _lastVertical = "LastVertical";
    private const string _lastHorizontal = "LastHorizontal";

    #region events
    private void OnEnable()
    {
        input.AttackEvent += OnAttack;
    }

    private void OnDisable()
    {
        input.AttackEvent -= OnAttack;
    }

    private void OnAttack()
    {
        StartCoroutine(Attack());
    }

    #endregion

    void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    IEnumerator Attack()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDir;

        mouseDir = mousePosition - transform.position;
        mouseDir.Normalize();

        _playerAnimator.SetFloat(_lastHorizontal, mouseDir.x);
        _playerAnimator.SetFloat(_lastVertical, mouseDir.y);

        _playerRigidBody.AddForce(mouseDir * _attackDrag);

        input.DisableMovement();
        input.DisableInteract();
        input.DisableAttack();

        _playerAnimator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.4f);

        input.EnableAttack();
        input.EnableMovement();
        input.EnableInteract();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth damageable = other.GetComponent<EnemyHealth>();
        Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
        if (damageable != null)
        {
            damageable.DamageKnockback(knockbackDirection);
        }
    }
}
