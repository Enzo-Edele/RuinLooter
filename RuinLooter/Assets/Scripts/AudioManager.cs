using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private float soundVolume;
    private float soundDistance;
    public AudioSource Sound;

    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SoundManager is NULL");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        soundVolume = Sound.volume;
    }

    public void Playsound(AudioClip audio)
    {
        Sound.PlayOneShot (audio, 1);
        Sound.Play();
    }

    public void SpacialSound(AudioClip audio, float distance)
    {
        Sound.clip = audio;
        soundDistance = soundVolume / distance;
        Sound.volume = soundDistance;
        if (soundDistance < soundVolume / 100)
        {
            Sound.volume = 0;
        }
    }
}
