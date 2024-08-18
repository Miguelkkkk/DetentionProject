using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{

    private Animator chestanimator;
    public bool isInChestRange;

    public CircleCollider2D InteractCollider { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Awake()
    {
        chestanimator = GetComponent<Animator>();
    }
    public void Update()
    {
        isInChestRange = GetComponentInChildren<Interactor>().isInRange;
    }
    public void OpenChest()
    {
        if (isInChestRange) { 
        chestanimator.SetTrigger("ChestOpened");
        }
    }

    public void Interact()
    {
        OpenChest();
    }
}
