using System.Collections;
using UnityEngine;

public class NPCInteraction : InteractableObject
{
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public new void Interact()
    {
        if (isInRange)
        {
            //coisas
        }
    }

   
    void Update()
    {
        isInRange = GetComponentInChildren<Interactor>().isInRange;
        if (isInRange)
        {
            UpdateOutline(true);
        }
        else
        {
            UpdateOutline(false);
        }
    }
}
