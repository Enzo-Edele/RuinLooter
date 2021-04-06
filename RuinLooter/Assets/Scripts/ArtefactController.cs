using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtefactController : MonoBehaviour
{
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
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ArtefactCollect(1);
            Destroy(gameObject);
        }
    }
}
