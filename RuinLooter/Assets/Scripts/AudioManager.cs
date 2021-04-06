using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
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
    }

    public void Playsound(AudioClip audio, bool spatial, float distance)
    {
        AudioSource Sound = gameObject.GetComponent<AudioSource>();
        Sound.clip = audio;
        if (spatial == true)
        {
            Sound.volume = 1 / distance;
        }
        if (Sound.isPlaying == false)
        {
            Sound.PlayOneShot(audio, Sound.volume);
        }
    }
}
