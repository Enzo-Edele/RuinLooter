using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtefactController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        player.ArtefactCollect();
        Destroy(gameObject);
    }
}
