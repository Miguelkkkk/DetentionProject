using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Input Reader", fileName ="Input Reader")]
public class InputReader : ScriptableObject
{
    [field: SerializeField] private InputActionAsset _inputasset;

    public event UnityAction<Vector2> MovementEvent;
    public event UnityAction InteractEvent;
    public event UnityAction AttackEvent;
    public event UnityAction RunEvent;
    public event UnityAction RunCancelledEvent;

    private InputAction _movementAction;
    private InputAction _interactAction;
    private InputAction _attackAction;
    private InputAction _runAction;

    private void OnEnable()
    {
        #region movement
        _movementAction = _inputasset.FindAction("Movement");
        
        _movementAction.started += OnMovement;
        _movementAction.performed += OnMovement;
        _movementAction.canceled += OnMovement;

        _movementAction.Enable();
        #endregion

        #region interact
        _interactAction = _inputasset.FindAction("Interact");

        _interactAction.started += OnInteract;
        _interactAction.performed += OnInteract;
        _interactAction.canceled += OnInteract;

        _interactAction.Enable();
        #endregion

        #region attack
        _attackAction = _inputasset.FindAction("Attack");

        _attackAction.started += OnAttack;
        _attackAction.performed += OnAttack;
        _attackAction.canceled += OnAttack;

        _attackAction.Enable();

        #endregion

        #region run

        _runAction = _inputasset.FindAction("Run");

        _runAction.started += OnRun;
        _runAction.performed += OnRun;
        _runAction.canceled += OnRun;

        _runAction.Enable();
        #endregion
    }

    private void OnDisable()
    {
        #region movement
        _movementAction.started -= OnMovement;
        _movementAction.performed -= OnMovement;
        _movementAction.canceled -= OnMovement;

        _movementAction.Disable();
        #endregion

        #region interact

        _interactAction.started -= OnInteract;
        _interactAction.performed -= OnInteract;
        _interactAction.canceled -= OnInteract;

        _interactAction.Disable();
        #endregion

        #region attack
        
        _attackAction.started -= OnAttack;
        _attackAction.performed -= OnAttack;
        _attackAction.canceled -= OnAttack;

        _attackAction.Disable();

        #endregion

        #region run

        _runAction.started -= OnRun;
        _runAction.performed -= OnRun;
        _runAction.canceled -= OnRun;

        _runAction.Disable();
        #endregion

    }

    private void OnMovement(InputAction.CallbackContext context) 
    {
        MovementEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (MovementEvent != null && context.started) {
            InteractEvent.Invoke();
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AttackEvent?.Invoke();  
        }
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        if (RunEvent != null && context.started)
        {
            RunEvent.Invoke();
        }

        if (RunCancelledEvent != null && context.canceled)
        {
            RunCancelledEvent.Invoke();
        }
    }
    public void EnableMovement() => _movementAction.Enable();
    public void DisableMovement() => _movementAction.Disable();

    public void EnableInteract() => _interactAction.Enable();
    public void DisableInteract() => _interactAction.Disable();

    public void EnableAttack() => _attackAction.Enable();
    public void DisableAttack() => _attackAction.Disable();

    public void EnableRun() => _runAction.Enable();
    public void DisableRun() => _runAction.Disable();
}
