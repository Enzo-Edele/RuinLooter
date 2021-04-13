using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource Sound;

    private static SoundManager _instance;
    public static SoundManager Instance
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
