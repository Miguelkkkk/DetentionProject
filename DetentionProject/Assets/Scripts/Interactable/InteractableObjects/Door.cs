using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{

    private Animator _doorAnimator;
    private bool isOpened = false;
    private SpriteRenderer _doorRenderer;
    [SerializeField] private bool isInRange;

    public void Awake()
    {
        _doorAnimator = GetComponent<Animator>();
        _doorRenderer = GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        isInRange = GetComponentInChildren<Interactor>().isInRange;
        if (isInRange && !isOpened)
        {
            _doorRenderer.material.SetFloat("_OutlineThickness", 1f);
        }
        else
        {
            _doorRenderer.material.SetFloat("_OutlineThickness", 0f);
        }
    }
    public void OpenDoor()
    {
        if (isInRange)
        {
            _doorAnimator.SetTrigger("DoorOpened");
            isOpened = true;
            _doorRenderer.material.SetFloat("_OutlineThickness", 0f);
        }
    }

    public void Interact()
    {
        OpenDoor();
    }
}

