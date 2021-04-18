using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    BoxCollider2D bc2d;
    PlayerController player;
    Animator animm;
    bool isOpen = false;
    bool isOn = false;
    public AudioClip sound;
    private void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        animm = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown("e") && !isOpen && isOn && GameManager.Instance.coin > 4)
        {
            this.Openning();
        }
        if(isOpen && bc2d.size.y > 0.2f)
        {
            Vector2 porte = bc2d.size;
            porte.y -= Time.deltaTime * 2;
            bc2d.size = porte;
        }
        if (isOpen && bc2d.offset.y > -2.1f)
        {
            Vector2 centre = bc2d.offset;
            centre.y -= Time.deltaTime * 2;
            bc2d.offset = centre;
        }
    }
    void Openning()
    {
        AudioManager.Instance.Playsound(sound, 0.5f);
        animm.SetTrigger("Openning");
        isOpen = true;
        player.CoinCollect(-5);
        animm.SetBool("Open", true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.GetComponent<PlayerController>();
        isOn = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isOn = false;
    }
}
