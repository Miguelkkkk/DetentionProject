using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    CircleCollider2D InteractCollider { get; set; }
    void Interact();
}
