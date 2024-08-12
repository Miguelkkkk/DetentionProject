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

    private InputAction _movementAction;
    private InputAction _interactAction;

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
}
