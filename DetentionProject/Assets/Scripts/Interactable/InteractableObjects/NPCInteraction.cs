using System.Collections;
using UnityEngine;

public class NPCInteraction : InteractableObject
{
    [SerializeField] private NPCDialogue dialogue;
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public new void Interact()
    {
        if (isInRange && dialogue.isInConversation == false)
        {
            dialogue.StartConversation();
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
