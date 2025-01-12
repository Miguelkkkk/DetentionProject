using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxDetection : MonoBehaviour
{
    [SerializeField] PlayerLife playerLife;
    private void OnTriggerStay2D(Collider2D collision)
    {
        playerLife.TriggerStay(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerLife.TriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerLife.TriggerExit(collision);
    }
}
