using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    // Cloup Enzo
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.isOnLadder = true;
            player.rb2d.gravityScale = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.isOnLadder = true;
            player.rb2d.bodyType = RigidbodyType2D.Static;
            player.rb2d.gravityScale = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.isOnLadder = false;
            player.rb2d.bodyType = RigidbodyType2D.Dynamic;
            player.rb2d.gravityScale = 1;
        }
    }
}
