using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] music;
    public Sound[] fx;

    public AudioMixer mainAudioMixer;
    public AudioMixerGroup musicAudioMixerGroup;
    public AudioMixerGroup FXAudioMixerGroup;

    [Range(0.0001f, 1f)]
    public float musicVolume;
    [Range(0.0001f, 1f)]
    public float fxVolume;

    public static AudioManager instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            PlayerPrefs.SetInt("volumneMaster", 30);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Make the object persisten between scenes
        DontDestroyOnLoad(this.gameObject);

        // Create an AudioSource for each music element and configure it
        foreach (Sound s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;

            // Set the output of the Audio Sources
            s.source.outputAudioMixerGroup = musicAudioMixerGroup;
        }

        // Create an AudioSource for each FX element and configure it
        foreach (Sound efecto in fx)
        {
            efecto.source = gameObject.AddComponent<AudioSource>();

            efecto.source.clip = efecto.clip;

            efecto.source.volume = efecto.volume;
            efecto.source.pitch = efecto.pitch;

            efecto.source.loop = efecto.loop;

            // Set the output of the Audio Sources
            efecto.source.outputAudioMixerGroup = FXAudioMixerGroup;
        }
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(music, s => s.name == name);

        if (s != null)
        {
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("Music " + name + " not found");
        }

    }
    public void PlayFX(string name)
    {
        Sound s = Array.Find(fx, s => s.name == name);

        if (s != null)
        {
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("Music " + name + " not found");
        }

    }
    private void Start()
    {
        mainAudioMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);
        mainAudioMixer.SetFloat("Effectos", Mathf.Log10(fxVolume) * 20);
    }

    public void StopMusic(string name)
    {

        Sound s = Array.Find(music, s => s.name == name);

        if (s != null)
        {
            s.source.Stop();
        }
        else
        {
            Debug.LogWarning("Music " + name + " not found");
        }
    }
    // Update is called once per frame
    void Update()
    {
        // To test the musicVolume variable
        //mainAudioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
    }
}
