using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource musicSource,sfxSource;
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private List<Sound> musicSounds = new(),sfxSounds = new ();
    Dictionary<string, AudioClip> musicSoundsMap = new(),sfxSoundsMap = new ();
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnValidate()
    {
        musicSoundsMap = new Dictionary<string, AudioClip>();
        sfxSoundsMap = new Dictionary<string, AudioClip>();
        foreach (var sound in musicSounds)
        {
            if(!musicSoundsMap.ContainsKey(sound.name)) musicSoundsMap[sound.name] = sound.clip;
            else Debug.LogError("Audio manager already contains music sound: " + sound.name);
        }
        foreach (var sound in sfxSounds)
        {
            if(!sfxSoundsMap.ContainsKey(sound.name)) sfxSoundsMap[sound.name] = sound.clip;
            else Debug.LogError("Audio manager already contains sfx sound: " + sound.name);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        AudioClip sound = musicSoundsMap[clip.name];
        if (sound == null)
        {
            Debug.LogError("Audio manager does not contain music sound: " + clip.name);
            return;
        }
        musicSource.clip = sound;
        musicSource.Play();
    }

    public void PlaySfx(AudioClip clip)
    {
        AudioClip sound = sfxSoundsMap[clip.name];
        if (sound == null)
        {
            Debug.LogError("Audio manager does not contain sfx sound: " + clip.name);
            return;
        }
        sfxSource.clip = sound;
        sfxSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        myMixer.SetFloat("music", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f);
    }

    public void SetSfxVolume(float volume)
    {
        myMixer.SetFloat("sfx", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f);
    }

}
