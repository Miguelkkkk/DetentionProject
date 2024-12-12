using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTrigger : MonoBehaviour
{
    public GameObject objectToActivate; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHitBox"))
        {
            objectToActivate.SetActive(true);
        }
    }
}