using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{

    private Animator _chestanimator;
    private bool isOpened = false;
    private SpriteRenderer _chestRenderer;
    [SerializeField]private bool isInRange;

    public void Awake()
    {
        _chestanimator = GetComponent<Animator>();
        _chestRenderer = GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        isInRange = GetComponentInChildren<Interactor>().isInRange;
        if(isInRange && !isOpened)
        {
            _chestRenderer.material.SetFloat("_OutlineThickness", 1f);
        }
        else
        {
            _chestRenderer.material.SetFloat("_OutlineThickness", 0f);
        }
    }
    public void OpenChest()
    {
        if (isInRange) {
            _chestanimator.SetTrigger("ChestOpened");
            isOpened = true;
            _chestRenderer.material.SetFloat("_OutlineThickness", 0f);
        }
    }

    public void Interact()
    {
        OpenChest();
    }
}
