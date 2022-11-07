using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    private AudioSource Audio;
    public static MusicManager instance;
    [SerializeField] private AudioMixerGroup Mixer;
    [SerializeField] private string nameKey;
    [SerializeField] private Slider SoundSlider;
    [Header("Настройки")]
    [SerializeField] private AudioClip[] audioClip;
    [Range(0f, -75f)]
    [SerializeField] private float MinDB;
    [Range(0f, 20f)]
    [SerializeField] private float MaxDB;

    public void Start()
    {
        Audio = GetComponent<AudioSource>();
        if(SoundSlider != null)
          SoundSlider.value = PlayerPrefs.GetFloat(nameKey, 1f);
        Mixer.audioMixer.SetFloat(nameKey, Mathf.Lerp(MinDB, MaxDB, PlayerPrefs.GetFloat(nameKey)));
    }
    // Чтобы запускать музыку один раз
    public void OnPlayOneShot(int number)
    {
       if(audioClip.Length != 0 & number <= audioClip.Length - 1)
       {
           Audio.PlayOneShot(audioClip[number]);
       }
    }
    // Чтобы узнавать ValueSlider
    public float InfoSlider()
    {
        return SoundSlider.value;
    }
    // Для Slider чтобы изменять громкость
    public void AllSoundsChangeVolume(float volume)
    {
        volume = SoundSlider.value;
        Mixer.audioMixer.SetFloat(nameKey, Mathf.Lerp(MinDB, MaxDB, volume));//значения заменить:)
        PlayerPrefs.SetFloat(nameKey, volume);
    }
    // Включения звука
    public void OnSound()
    {
        Audio.mute = true;
    }
    // Выключение звука
    public void OffSound()
    {
        Audio.mute = false;
    }
}
