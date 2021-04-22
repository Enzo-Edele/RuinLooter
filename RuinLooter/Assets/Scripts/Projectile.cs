using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Le Bacquer Alexandre
    public virtual void Launch(Vector2 direction, float force)
    {
        if (transform.position.magnitude > 60)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Damage()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
