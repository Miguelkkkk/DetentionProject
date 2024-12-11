using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInteractable : InteractableObject
{
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public new void Interact()
    {
        if (isInRange) { 
            PlayerData player = new PlayerData(null, true);
            SaveManager.SavePlayerData(player);
            this.gameObject.SetActive(false);
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
