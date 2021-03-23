using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : Enemy
{
    Rigidbody2D rigidbody2d;
    public float delay = 3f;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.Damage(-1);
            StartCoroutine(TimeInvincible());
        }
    }

    private IEnumerator TimeInvincible()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(7, 8, true);
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
