using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Player Input Reader", fileName = "Input Reader")]
public class PlayerInputReader : InputReader
{

    public event UnityAction<Vector2> MovementEvent;
    public event UnityAction InteractEvent;
    public event UnityAction DodgeEvent;
    public event UnityAction AttackEvent;
    public event UnityAction RunEvent;
    public event UnityAction RunCancelledEvent;

    private InputAction _movementAction;
    private InputAction _interactAction;
    private InputAction _dodgeAction;
    private InputAction _runAction;
    private InputAction _attackAction;

    protected override void OnEnable()
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

        #region run

        _runAction = _inputasset.FindAction("Run");

        _runAction.started += OnRun;
        _runAction.performed += OnRun;
        _runAction.canceled += OnRun;

        _runAction.Enable();
        #endregion

        #region dodge

        _dodgeAction = _inputasset.FindAction("DodgeRoll");

        _dodgeAction.started += OnDodge;
        _dodgeAction.performed += OnDodge;
        _dodgeAction.canceled += OnDodge;

        _dodgeAction.Enable();
        #endregion

        #region attack

        _attackAction = _inputasset.FindAction("Attack");

        _attackAction.started += OnAttack;
        _attackAction.performed += OnAttack;
        _attackAction.canceled += OnAttack;

        _attackAction.Enable();
        #endregion
    }

    protected override void OnDisable()
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

        #region run

        _runAction.started -= OnRun;
        _runAction.performed -= OnRun;
        _runAction.canceled -= OnRun;

        _runAction.Disable();
        #endregion

        #region dodge

        _dodgeAction.started -= OnDodge;
        _dodgeAction.performed -= OnDodge;
        _dodgeAction.canceled -= OnDodge;

        _dodgeAction.Disable();
        #endregion

        #region attack

        _attackAction.started -= OnAttack;
        _attackAction.performed -= OnAttack;
        _attackAction.canceled -= OnAttack;

        _attackAction.Disable();
        #endregion
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        MovementEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (AttackEvent != null && context.started)
        {
            AttackEvent.Invoke();
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (MovementEvent != null && context.started)
        {
            InteractEvent.Invoke();
        }
    }

    private void OnDodge(InputAction.CallbackContext context) 
    {
        if (DodgeEvent != null && context.started)
        {
            DodgeEvent.Invoke();
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

    public void DisablePlayerAction()
    {
        _inputasset.FindActionMap("Player").Disable();
    }

    public void EnablePlayerAction()
    {
        _inputasset.FindActionMap("Player").Enable();
    }
}