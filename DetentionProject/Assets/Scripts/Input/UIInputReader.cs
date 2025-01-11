using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;



[CreateAssetMenu(menuName = "Events/UI Input Reader", fileName = "Input Reader")]
public class UIInputReader : InputReader
{
    public event UnityAction SkipDialogueEvent;
    public event UnityAction UpEvent;
    public event UnityAction DownEvent;
    public event UnityAction LeftEvent;
    public event UnityAction RightEvent;
    public event UnityAction ConfirmEvent;

    private InputAction _confirmAction;
    private InputAction _upAction;
    private InputAction _downAction;
    private InputAction _leftAction;
    private InputAction _rightAction;
    private InputAction _skipDialogueAction;

    protected override void OnEnable()
    {
        #region SkipDialogue
        _skipDialogueAction = _inputasset.FindAction("SkipDialogue");
            
        _skipDialogueAction.started += OnSkipDialogue;
        _skipDialogueAction.performed += OnSkipDialogue;
        _skipDialogueAction.canceled += OnSkipDialogue;

        _skipDialogueAction.Enable();
        #endregion

        #region Up
        _upAction = _inputasset.FindAction("Up");

        _upAction.started += OnUpPressed;
        _upAction.performed += OnUpPressed;
        _upAction.canceled += OnUpPressed;

        _upAction.Enable();
        #endregion

        #region Down
        _downAction = _inputasset.FindAction("Down");

        _downAction.started += OnDownPressed;
        _downAction.performed += OnDownPressed;
        _downAction.canceled += OnDownPressed;

        _downAction.Enable();
        #endregion

        #region Left
        _leftAction = _inputasset.FindAction("Left");

        _leftAction.started += OnLeftPressed;
        _leftAction.performed += OnLeftPressed;
        _leftAction.canceled += OnLeftPressed;

        _leftAction.Enable();
        #endregion

        #region Right
        _rightAction = _inputasset.FindAction("Right");

        _rightAction.started += OnRightPressed;
        _rightAction.performed += OnRightPressed;
        _rightAction.canceled += OnRightPressed;

        _rightAction.Enable();
        #endregion

        #region Confirm
        _confirmAction = _inputasset.FindAction("Confirm");

        _confirmAction.started += OnConfirm;
        _confirmAction.performed += OnConfirm;
        _confirmAction.canceled += OnConfirm;

        _confirmAction.Enable();
        #endregion

    }

    protected override void OnDisable()
    {
        #region SkipDialogue

        _skipDialogueAction.started -= OnSkipDialogue;
        _skipDialogueAction.performed -= OnSkipDialogue;
        _skipDialogueAction.canceled -= OnSkipDialogue;

        _skipDialogueAction.Disable();
        #endregion

        #region Up

        _upAction.started -= OnUpPressed;
        _upAction.performed -= OnUpPressed;
        _upAction.canceled -= OnUpPressed;

        _upAction.Disable();
        #endregion

        #region Down

        _downAction.started -= OnDownPressed;
        _downAction.performed -= OnDownPressed;
        _downAction.canceled -= OnDownPressed;

        _downAction.Disable();
        #endregion

        #region Left

        _leftAction.started -= OnLeftPressed;
        _leftAction.performed -= OnLeftPressed;
        _leftAction.canceled -= OnLeftPressed;

        _leftAction.Disable();
        #endregion

        #region Right

        _rightAction.started -= OnRightPressed;
        _rightAction.performed -= OnRightPressed;
        _rightAction.canceled -= OnRightPressed;

        _rightAction.Disable();
        #endregion

        #region Confirm

        _confirmAction.started -= OnConfirm;
        _confirmAction.performed -= OnConfirm;
        _confirmAction.canceled -= OnConfirm;

        _confirmAction.Disable();
        #endregion
    }

    private void OnUpPressed(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            UpEvent?.Invoke();
        }
    }
    private void OnRightPressed(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            RightEvent?.Invoke();
        }
    }
    private void OnDownPressed(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            DownEvent?.Invoke();
        }
    }
    private void OnLeftPressed(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            LeftEvent?.Invoke();
        }
    }

    private void OnSkipDialogue(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            SkipDialogueEvent?.Invoke();
        }
    }
    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            ConfirmEvent?.Invoke();
        }
    }

    public void DisableUIAction()
    {
        _inputasset.FindActionMap("UI").Disable();
    }

    public void EnableUIAction()
    {
        _inputasset.FindActionMap("UI").Enable();
    }

}
