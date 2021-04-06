using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> audios = new List<AudioSource>();
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
        DontDestroyOnLoad(this.gameObject);
    }

    public void Playsound(AudioClip audio, bool spatial, float distance)
    {
        bool soundCreate = true;
        for (int i = 0; i < audios.Count; i++)
        {
            if (audios[i] == null)
            {
                audios.Remove(audios[i]);
            }
            if (audios[i].clip == audio)
            {
                if (spatial == true)
                {
                    audios[i].volume = 1 / distance;
                    Destroy(audios[i], audio.length);
                }
                soundCreate = false;
            }
        }

        if (soundCreate == true)
        {
            AudioSource Sound = gameObject.AddComponent<AudioSource>();
            audios.Add(Sound);
            Sound.clip = audio;
            Sound.PlayOneShot(audio, Sound.volume);
            Destroy(Sound, audio.length);
        }
    }
}
