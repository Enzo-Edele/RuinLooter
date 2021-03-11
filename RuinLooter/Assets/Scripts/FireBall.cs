using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Projectile
{
    Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public override void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
        if (transform.position.magnitude > 30)
        {
            Destroy(gameObject);
        }
    }
}
