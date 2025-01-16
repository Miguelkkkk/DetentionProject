using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Audio; 

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; 
    [SerializeField] private AudioMixer audioMixer; 
    [SerializeField] private string volumeParameter = "MasterVolume"; 

    private void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
            LoadSavedVolume(); 
        }
    }

    private void SetVolume(float sliderValue)
    {
        float volumeInDb = Mathf.Log10(sliderValue) * 20;
        audioMixer.SetFloat(volumeParameter, volumeInDb);
    }

    private void LoadSavedVolume()
    {
        if (PlayerPrefs.HasKey(volumeParameter))
        {
            float savedValue = PlayerPrefs.GetFloat(volumeParameter);
            volumeSlider.value = savedValue; 
            SetVolume(savedValue); 
        }
        else
        {
            volumeSlider.value = 1f; 
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, volumeSlider.value);
    }
}
