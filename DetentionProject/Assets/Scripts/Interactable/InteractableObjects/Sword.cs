using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInteractable : InteractableObject
{
    public GameObject swordObject;
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

        PlayerData loadedPlayer = SaveManager.LoadPlayerData();
        if (loadedPlayer != null)
        {
            if (loadedPlayer.hasTakenSword)
            {
                this.gameObject.SetActive(false);
            }
        }
        }


    public new void Interact()
    {
        if (isInRange) { 
            PlayerData player = new PlayerData(null, true);
            SaveManager.SavePlayerData(player);
            swordObject.SetActive(true);
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
