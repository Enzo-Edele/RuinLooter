using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> audios = new List<AudioSource>();
    private static AudioManager _instance;
    public AudioClip sound;
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

    public void Playsound(AudioClip audio, float volume)
    {
        bool soundCreate = true;
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i] == null)
            {
                audios.Remove(audios[i]);
            }
        }
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i].clip == audio)
            {
                soundCreate = false;
            }
        }

        if (soundCreate == true)
        {
            AudioSource Sound = gameObject.AddComponent<AudioSource>();
            audios.Add(Sound);
            Sound.clip = audio;
            Sound.volume = volume;
            Sound.PlayOneShot(audio);
            Destroy(Sound, audio.length);
        }
    }


    public void StopAllSounds()
    {
        for (int i = 0; i < audios.Count; i++)
        {
            Destroy(audios[i]);
        }
    }

    public void SoundEndLevel()
    {
        AudioManager.Instance.Playsound(sound, 1);
    }
}