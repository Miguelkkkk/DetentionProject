using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxStamina(float newMaxStamina)
    {
        slider.maxValue = newMaxStamina;
    }

    public void SetStamina(float stamina)
    {
        slider.value = stamina;
    }

    public void UpdateStamina(Component sender, object data)
    {
        if (data is float stamina)
        {
            SetStamina(stamina);    
        }
    }
}
