//Author: Small Hedge Games
//Updated: 13/06/2024

using System;
using UnityEngine;
using UnityEngine.Audio;

namespace SmallHedge.SoundManager
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private SoundsSO SO;
        private static SoundManager instance = null;
        private AudioSource audioSource;

        private void Awake()
        {
            if (!instance)
            {
                instance = this;
                audioSource = GetComponent<AudioSource>();
            }
        }

        public static void PlaySound(SoundType sound, AudioSource source = null, float volume = 1)
        {
            SoundList soundList = instance.SO.sounds[(int)sound];
            AudioClip[] clips = soundList.sounds;
            AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

            float pitch = UnityEngine.Random.Range(soundList.pitchMin, soundList.pitchMax);

            if (source)
            {
                source.outputAudioMixerGroup = soundList.mixer;
                source.clip = randomClip;
                source.volume = volume * soundList.volume;
                source.pitch = pitch;
                source.Play();
            }
            else
            {
                instance.audioSource.outputAudioMixerGroup = soundList.mixer;
                instance.audioSource.pitch = pitch;
                instance.audioSource.PlayOneShot(randomClip, volume * soundList.volume);
            }
        }
    }

    [Serializable]
    public struct SoundList
    {
        [HideInInspector] public string name;
        [Range(0, 1)] public float volume;
        public AudioMixerGroup mixer;
        public AudioClip[] sounds;
        [Range(0.5f, 2f)] public float pitchMin;
        [Range(0.5f, 2f)] public float pitchMax; 
    }
}
