using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using SaveManagerLibrary;

public class MusicManager : MonoBehaviour
{
    private AudioSource Audio;
    [SerializeField] private AudioMixerGroup Mixer;
    [SerializeField] private string nameKey;
    [SerializeField] private Slider SoundSlider;
    [Header("Настройки")]
    [SerializeField] private AudioClip[] audioClip;
    [Range(0f, -80f)]
    [SerializeField] private float MinDB;
    [Range(0f, 20f)]
    [SerializeField] private float MaxDB;
    private float currentSound;
    public void Start()
    {
        Audio = GetComponent<AudioSource>();
        SoundSlider.value = PlayerPrefs.GetFloat(nameKey, 1f);
        Mixer.audioMixer.SetFloat(nameKey, Mathf.Lerp(MinDB, MaxDB, PlayerPrefs.GetFloat(nameKey)));
        OnPlayOneShot(0);
    }
    public void OnPlayOneShot(int number)
    {
       if(audioClip != null)
         Audio.PlayOneShot(audioClip[number]);
    }
    public float InfoSlider()
    {
        return SoundSlider.value;
    }
    public void AllSoundsChangeVolume(float volume)
    {
        volume = SoundSlider.value;
        Mixer.audioMixer.SetFloat(nameKey, Mathf.Lerp(MinDB, MaxDB, volume));//значения заменить:)
        PlayerPrefs.SetFloat(nameKey, volume);
    }
    public void OnSound()
    {
        SoundSlider.value = currentSound;
    }
    public void OffSound()
    {
        currentSound = SoundSlider.value;
        Mixer.audioMixer.SetFloat(nameKey, SoundSlider.minValue);
        SoundSlider.value = SoundSlider.minValue;
    }
}
