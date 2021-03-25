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
