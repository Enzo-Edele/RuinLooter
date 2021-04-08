using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public AudioClip sound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioManager.Instance.Playsound(sound, 0.2f);
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.CoinCollect(1);
        }
        Destroy(gameObject);
    }
}
