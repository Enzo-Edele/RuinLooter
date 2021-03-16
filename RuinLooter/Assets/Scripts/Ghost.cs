using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    Vector2 spawnPoint;
    public float speed = 2f;
    public float distToSpawnPoint = 10;
    private float ghostPositionX;
    private float ghostPositionY;
    public float delay = 3;
    private bool ghostMove = true;
    Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        ghostPositionX = Random.Range(distToSpawnPoint, -distToSpawnPoint);
        ghostPositionY = Random.Range(distToSpawnPoint, -distToSpawnPoint);

        if (transform.position.x > spawnPoint.x || transform.position.y > spawnPoint.y)
        {
            ghostPositionX = Random.Range(-1, -distToSpawnPoint);
        }
        if (transform.position.x < spawnPoint.x || transform.position.y < spawnPoint.y)
        {
            ghostPositionX = Random.Range(1, distToSpawnPoint);
        }

        Vector2 dir = new Vector2(ghostPositionX, ghostPositionY);

        if (ghostMove == true)
        {
            rb2d.AddForce(dir * 30);
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        ghostMove = false;
        yield return new WaitForSeconds(delay);
        rb2d.velocity = Vector3.zero;
        yield return new WaitForSeconds(delay);
        ghostMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            Debug.Log(player.PV);
        }
    }
}
