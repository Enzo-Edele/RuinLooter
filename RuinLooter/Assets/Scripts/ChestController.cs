using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    bool isOpen = false;
    bool isOn = false;
    PlayerController player;
    private void Update()
    {
        if(Input.GetKeyDown("e") && !isOpen && isOn)
        {
            this.Openning();
        }
    }
    void Openning()
    {
        isOpen = true;
        if(player != null)
        {
            player.ChestOpenning();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        isOn = true;
        player = other.GetComponent<PlayerController>();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isOn = false;
        player = null;
    }
}
