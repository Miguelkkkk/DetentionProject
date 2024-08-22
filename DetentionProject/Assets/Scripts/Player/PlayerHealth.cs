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
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(1);
            CinemachineShake.Instance.shakeCamera(6f, .2f);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        onPlayerHealthChanged.Raise(this, currentHealth);
    }
}

