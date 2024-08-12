using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    void Start()
    {
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }

    public void SetHealth(float health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
        slider.value = currentHealth;
    }
    public void UpdateHealth(float health) {

        if (currentHealth + health < maxHealth)
        {
            currentHealth += health;
        }
        else {
            currentHealth = maxHealth;
        }
        slider.value = currentHealth;
    }
}
