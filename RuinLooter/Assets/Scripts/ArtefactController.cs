using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtefactController : MonoBehaviour
{
    public AudioClip sound;
    [HideInInspector]
    public float x;
    [HideInInspector]
    public float y;
    private void Start()
    {
        Vector2 pos = transform.position;
        x = pos.x;
        y = pos.y;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        AudioManager.Instance.Playsound(sound, 0.2f);
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ArtefactCollect(1);
            Destroy(gameObject);
        }
    }
}
