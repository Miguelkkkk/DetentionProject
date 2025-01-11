using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Input")]
    public PlayerInputReader input;

    [Header("Events")]
    public GameEvent onPlayerInteracted;

    #region events
    private void OnEnable()
    {
        input.InteractEvent += onInteract;
    }

    private void OnDisable()
    {
        input.InteractEvent -= onInteract;
    }

    private void onInteract() {
        onPlayerInteracted.Raise(this);
    }
    
    #endregion
}
