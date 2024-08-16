using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int newMaxHealth)
    {
        slider.maxValue = newMaxHealth;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void UpdateHealth(Component sender, object data)
    {
        if (data is int health)
        {
            SetHealth(health);
        }
    }
}
