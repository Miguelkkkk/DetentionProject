using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Input")]
    public InputReader Input;

    [Header("Events")]
    public GameEvent onPlayerInteracted;

    #region events
    private void OnEnable()
    {
        Input.InteractEvent += onInteract;
    }

    private void OnDisable()
    {
        Input.InteractEvent -= onInteract;
    }

    private void onInteract() {
        onPlayerInteracted.Raise(this);
    }
    
    #endregion
}
