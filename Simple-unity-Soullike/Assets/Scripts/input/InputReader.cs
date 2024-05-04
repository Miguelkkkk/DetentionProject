using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Input/Input Reader", fileName ="Input Reader")]
public class InputReader : ScriptableObject
{
    [field: SerializeField] private InputActionAsset _inputasset;

    public event UnityAction<Vector2> MovementEvent;

    private InputAction _movementAction;

    private void OnEnable()
    {
        _movementAction = _inputasset.FindAction("Movement");

        _movementAction.started += OnMovement;
        _movementAction.performed += OnMovement;
        _movementAction.canceled += OnMovement;

        _movementAction.Enable();
    }

    private void OnDisable()
    {
        _movementAction.started -= OnMovement;
        _movementAction.performed -= OnMovement;
        _movementAction.canceled -= OnMovement;

        _movementAction.Disable();
    }

    private void OnMovement(InputAction.CallbackContext context) 
    {
        MovementEvent?.Invoke(context.ReadValue<Vector2>());
    }
}
