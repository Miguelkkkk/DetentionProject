using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{

    private Animator _chestanimator;
    [SerializeField]private bool isInChestRange;

    public CircleCollider2D InteractCollider { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Awake()
    {
        _chestanimator = GetComponent<Animator>();
    }
    public void Update()
    {
        isInChestRange = GetComponentInChildren<Interactor>().isInRange;
    }
    public void OpenChest()
    {
        if (isInChestRange) {
            _chestanimator.SetTrigger("ChestOpened");
        }
    }

    public void Interact()
    {
        OpenChest();
    }
}
