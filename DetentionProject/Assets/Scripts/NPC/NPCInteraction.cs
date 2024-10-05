using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    private bool isInRange;
    private SpriteRenderer _npcRenderer;

    private void Start()
    {
        _npcRenderer = GetComponent<SpriteRenderer>();
    }

    public void Interact()
    {
        //código de interação.
    }

    // Update is called once per frame
    void Update()
    {
        isInRange = GetComponentInChildren<Interactor>().isInRange;
        if (isInRange)
        {
            _npcRenderer.material.SetFloat("_OutlineThickness", 1f);
        }
        else {
            _npcRenderer.material.SetFloat("_OutlineThickness", 0f);
        }
    }
}
