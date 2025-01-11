using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : ScriptableObject
{
    [field: SerializeField] protected InputActionAsset _inputasset;
    protected virtual void OnEnable()
    {
  
    }

    protected virtual void OnDisable()
    {
     
    }

    public void DisableActionMap()
    {
        _inputasset.Disable();
    }

    public void EnableAcionMap()
    {
        _inputasset.Enable();
    }
}
