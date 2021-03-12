using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField]
    Transform player;

    public float agroRange;
    private float spawnPoint;
    public float speed = 0.005f;
    private float speedagro;
    public float distance = 4f;

    void Start()
    {
        spawnPoint = transform.position.x;
        speedagro = speed;
    }

    void Update()
    {
        Vector3 snakeScale = transform.localScale;
        transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < agroRange)
        {
            if (transform.position.x < player.position.x)
            {
                snakeScale.x = -1;
                speed = -speedagro * 10;
            }
            else
            {
                snakeScale.x = 1;
                speed = speedagro * 10;
            }

            transform.localScale = snakeScale;
        }

        else if (transform.position.x < spawnPoint - distance || transform.position.x > spawnPoint + distance)
        {
            spawnPoint = transform.position.x;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.collider.GetComponent<PlayerController>();
            player.Damage(-1);
        }
    }
}