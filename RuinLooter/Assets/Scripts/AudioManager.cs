using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
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
    }

    public void Playsound(AudioClip clip)
    {
        Sound.clip = clip;
        Sound.Play();
    }
}
