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

    private float currentStamina;
    private Coroutine regenCoroutine;

    private void Awake()
    {
        currentStamina = maxStamina;
    }

    public bool CanUseStamina()
    {
        return currentStamina > 0;
    }

    public void UseStamina()
    {
        currentStamina -= staminaUseRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        onStaminaChanged.Raise(this, currentStamina);

        if (regenCoroutine != null)
        {
            StopCoroutine(regenCoroutine); // Para a regeneração enquanto está gastando stamina.
            regenCoroutine = null;
        }
    }

    public void StartRegen()
    {
        if (regenCoroutine == null)
        {
            regenCoroutine = StartCoroutine(RegenStamina());
        }
    }

    private IEnumerator RegenStamina()
    {
        while (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            onStaminaChanged.Raise(this, currentStamina);
            yield return null;
        }
        regenCoroutine = null;
    }
}
