using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Background Music")]
    public AudioSource musicSource;  // Drag your background music AudioSource here

    [Header("Sound Effects")]
    public AudioSource sfxSource;  // General AudioSource for playing sound effects
    public AudioClip[] soundEffects;  // Array of all sound effects

    public enum SoundEffect
    {
        Chyme,
        Hit,
        Hit2,
        OvenDing,
        Skiing,
        Swipe,
        Whistle,
        Whoosh,
        Wrong
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Keep the audio manager persistent across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundEffect(SoundEffect effect)
    {
        if (sfxSource != null && soundEffects.Length > (int)effect)
        {
            sfxSource.PlayOneShot(soundEffects[(int)effect]);
        }
    }

    public void ToggleMute()
    {
        bool isMuted = musicSource.mute;
        musicSource.mute = !isMuted;
        sfxSource.mute = !isMuted;
    }

    public void PlayBackgroundMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}