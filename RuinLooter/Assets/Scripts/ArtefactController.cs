using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtefactController : MonoBehaviour
{
    Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        player.ArtefactCollect();
        Destroy(gameObject);
    }
}
