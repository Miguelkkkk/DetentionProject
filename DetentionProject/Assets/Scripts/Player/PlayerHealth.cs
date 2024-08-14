using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{

    [Header("Events")]
    public GameEvent onHealthChanged;

    [Header("Health")]
    [SerializeField] private int maxHealth;
    private int currentHealth = 0;

    public void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        onHealthChanged.Raise(this, (float)currentHealth);
        Debug.Log("foi");
    }
}
