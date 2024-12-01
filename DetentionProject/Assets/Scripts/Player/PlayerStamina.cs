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
    private Coroutine regenDelayCoroutine;

    private void Awake()
    {
        currentStamina = maxStamina;
    }

    public float GetCurrentStamina()
    {
        return currentStamina;
    }

    public void UseStamina()
    {
        currentStamina -= staminaUseRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        onStaminaChanged.Raise(this, currentStamina);

        StopRegen();
    }

    public void UpdateStamina(float value)
    {
        currentStamina += value;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        onStaminaChanged.Raise(this, currentStamina);

        StopRegen();

        if (regenDelayCoroutine != null)
        {
            StopCoroutine(regenDelayCoroutine);
        }
        regenDelayCoroutine = StartCoroutine(StartRegenDelay(3f));
    }

    private void StopRegen()
    {
        if (regenCoroutine != null)
        {
            StopCoroutine(regenCoroutine);
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

    private IEnumerator StartRegenDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartRegen();
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
