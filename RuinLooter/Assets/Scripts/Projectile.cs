using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            Destroy(gameObject);
            player.Damage(-1);
        }
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    public virtual void Launch(Vector2 direction, float force)
    {
        if (transform.position.magnitude > 60)
        {
            Destroy(gameObject);
        }
    }
}
