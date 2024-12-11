using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    protected Animator _animator;
    protected bool hasInteracted = false;
    protected SpriteRenderer _renderer;
    protected bool isInRange;
    public void Interact()
    {
        Debug.Log("Interagiu");  
    }

    protected void UpdateOutline(bool isActive)
    {
        if (isActive)
        {
            _renderer.material.SetFloat("_OutlineThickness", 1f);
        }
        else
        {
            _renderer.material.SetFloat("_OutlineThickness", 0f);
        }
    }

}
