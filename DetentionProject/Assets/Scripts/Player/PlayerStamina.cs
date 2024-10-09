using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [Header("Events")]
    public GameEvent onStaminaChanged;

    [Header("Stamina")]
    [SerializeField] private int maxStamina;
    [SerializeField] private float staminaRegenRate;
    [SerializeField] private float staminaUseRate;

    private float currentStamina = 0;


    public void Awake()
    {
        currentStamina = maxStamina;
    }

    public void UseStamina(int ammount)
    {
        currentStamina -= staminaUseRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        onStaminaChanged.Raise(this, (float)currentStamina);
    }

    IEnumerator RegenStamina()
    {
        while (true)
        {
            if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
                onStaminaChanged.Raise(this, (float)currentStamina);
            }
            yield return null; // Aguarda o próximo frame antes de continuar
        }
    }
}
