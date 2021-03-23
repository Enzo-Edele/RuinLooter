using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    Vector2 spawnPoint;
    public float speed = 2f;
    public float moveZone = 10;
    private float ghostPositionX;
    private float ghostPositionY;
    public float delay = 5;
    private bool ghostMove = true;
    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        ghostPositionX = Random.Range(moveZone, -moveZone);
        ghostPositionY = Random.Range(moveZone, -moveZone);

        if (transform.position.x > spawnPoint.x)
        {
            ghostPositionX = Random.Range(-1, -moveZone);
        }
        if (transform.position.y > spawnPoint.y)
        {
            ghostPositionY = Random.Range(-1, -moveZone);
        }
        if (transform.position.x < spawnPoint.x)
        {
            ghostPositionX = Random.Range(1, moveZone);
        }
        if (transform.position.y < spawnPoint.y)
        {
            ghostPositionY = Random.Range(1, moveZone);
        }

        Vector2 dir = new Vector2(ghostPositionX, ghostPositionY);

        if (ghostMove == true)
        {
            rb2d.velocity = Vector3.zero;
            rb2d.AddForce(dir * 30);
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        ghostMove = false;
        yield return new WaitForSeconds(delay);
        ghostMove = true;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.collider.GetComponent<PlayerController>();
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
}
