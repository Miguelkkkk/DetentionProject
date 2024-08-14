using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    //futaramente inicializar a max health durante o awake em algum lugar do código
    public void SetMaxHealth(float newMaxHealth)
    {
        slider.maxValue = newMaxHealth;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
