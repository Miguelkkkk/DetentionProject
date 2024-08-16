using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{

    [Header("Events")]
    public GameEvent onPlayerHealthChanged;

    [Header("Health")]
    [SerializeField] private int maxHealth;
    private int currentHealth = 0;

    public void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Update()
    {
        //teste de dano
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Evento raise chamado com currentHealth: " + currentHealth);
        onPlayerHealthChanged.Raise(this, currentHealth);
    }
}

