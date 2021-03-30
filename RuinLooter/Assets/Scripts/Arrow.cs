using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public override void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
}