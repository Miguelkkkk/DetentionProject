using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NPCInteraction : InteractableObject
{
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public new void Interact()
    {
        //código de interação.
    }

    void Update()
    {
        isInRange = GetComponentInChildren<Interactor>().isInRange;
        if (isInRange)
        {
            UpdateOutline(true);
        }
        else {
            UpdateOutline(false);
        }
    }
}
