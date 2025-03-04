using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumenSlider : MonoBehaviour
{

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Master Volume")]
    public Slider volumeSlider;
    public string exposedMasterParam;

    [Header("Music Volume")]
    public Slider musicSlider;
    public string exposedMusicParam;

    [Header("FX Volume")]
    public Slider fxSlider;
    public string exposedFXParam;

    private void Start()
    {
        // Inicializar sliders con valores almacenados o predeterminados
        InitializeSlider(volumeSlider, exposedMasterParam, "MasterVolumePref", 0f);
        InitializeSlider(musicSlider, exposedMusicParam, "MusicVolumePref", 0f);
        InitializeSlider(fxSlider, exposedFXParam, "FXVolumePref", 0f);

        // Asignar listeners a los sliders
        volumeSlider.onValueChanged.AddListener(value => SetVolume(value, exposedMasterParam, "MasterVolumePref"));
        musicSlider.onValueChanged.AddListener(value => SetVolume(value, exposedMusicParam, "MusicVolumePref"));
        fxSlider.onValueChanged.AddListener(value => SetVolume(value, exposedFXParam, "FXVolumePref"));
    }

    private void InitializeSlider(Slider slider, string exposedParam, string playerPrefKey, float defaultValue)
    {
        // Recuperar el volumen almacenado en PlayerPrefs o usar el valor predeterminado
        float savedValue = PlayerPrefs.GetFloat(playerPrefKey, defaultValue);
        slider.value = savedValue;
        audioMixer.SetFloat(exposedParam, savedValue);
    }

    private void SetVolume(float value, string exposedParam, string playerPrefKey)
    {
        // Ajustar el volumen en el Audio Mixer y guardar en PlayerPrefs
        audioMixer.SetFloat(exposedParam, value);
        PlayerPrefs.SetFloat(playerPrefKey, value);
    }
}
