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
            StartCoroutine(TimeInvincible());
        }
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
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

    public virtual void Launch(Vector2 direction, float force)
    {
        if (transform.position.magnitude > 60)
        {
            Destroy(gameObject);
        }
    }
}
