using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator chestanimator;
    public void Awake()
    {
        chestanimator = GetComponent<Animator>();
    }
    public void OpenChest()
    {
        
    }
}
