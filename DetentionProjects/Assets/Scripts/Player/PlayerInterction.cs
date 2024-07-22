using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterction : MonoBehaviour
{
    [Header("Input")]
    public InputReader Input;

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
        Debug.Log("interacted");
    }
    
    #endregion
}
