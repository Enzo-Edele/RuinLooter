using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField]
    Transform player;

    Rigidbody2D rb2d;

    public float agroRange;
    private float spawnPoint;
    public float speed = 0.005f;
    public float distance = 4f;

    void Start()
    {
        spawnPoint = transform.position.x;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 snakeScale = transform.localScale;
        transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < agroRange)
        {
            Debug.Log("agro");
            if (transform.position.x < player.position.x)
            {
                rb2d.velocity = new Vector2(-speed, 0);
                snakeScale.x = -1;
            }
            else
            {
                rb2d.velocity = new Vector2(speed, 0);
                snakeScale.x = 1;
            }
        }
        else
        {
            if (transform.position.x < spawnPoint - distance || transform.position.x > spawnPoint + distance)
            {
                speed = speed * -1;

                if (speed < 0)
                {
                    snakeScale.x = -1;
                }
                else
                {
                    snakeScale.x = 1;
                }
                transform.localScale = snakeScale;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("fireBall"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Damage();
        }
    }

    void Damage()
    {
        Debug.Log("dégats");
    }
}