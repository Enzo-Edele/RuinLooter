using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtefactController : MonoBehaviour
{
    // Cloup Enzo

    public AudioClip sound;
    public Sprite armature;
    public Sprite engrenage;
    public Sprite cadran;
    public int part = 1;
    SpriteRenderer inUse;
    [HideInInspector]
    public float x;
    [HideInInspector]
    public float y;
    private void Start()
    {
        Vector2 pos = transform.position;
        x = pos.x;
        y = pos.y;
        inUse = GetComponent<SpriteRenderer>();
        this.Apparence();
    }
    void Apparence()
    {
        switch(part)
        {
            case 1:
                inUse.sprite = armature;
                break;
            case 2:
                inUse.sprite = engrenage;
                break;
            case 3:
                inUse.sprite = cadran;
                break;
        }

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
