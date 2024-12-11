using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableObject
{
    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        isInRange = GetComponentInChildren<Interactor>().isInRange;
        if(isInRange && !hasInteracted)
        {
            UpdateOutline(true);
        }
        else
        {
            UpdateOutline(false);
        }
    }
    public void OpenChest()
    {
        if (isInRange) {
            _animator.SetTrigger("ChestOpened");
            hasInteracted = true;
            UpdateOutline(false);
        }
    }

   
    public new void Interact()
    {
        OpenChest();
    }
}
