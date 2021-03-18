using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField]
    Transform player;

    public float agroRange;
    private float spawnPoint;
    public float speed = 0.005f;
    private float speedagro;
    public float distance = 4f;
    public float agroSpeed = 1.2f;

    void Start()
    {
        spawnPoint = transform.position.x;
        speedagro = speed;
    }

    void Update()
    {
        Vector3 robotScale = transform.localScale;
        transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < agroRange)
        {
            if (transform.position.x < player.position.x)
            {
                robotScale.x = -2;
                speed = -speedagro * agroSpeed;
            }
            else
            {
                robotScale.x = 2;
                speed = speedagro * agroSpeed;
            }

            transform.localScale = robotScale;
        }

        else if (transform.position.x < spawnPoint - distance || transform.position.x > spawnPoint + distance)
        {
            spawnPoint = transform.position.x;
            speed = speed * -1;
            if (speed < 0)
            {
                robotScale.x = -2;
            }
            else
            {
                robotScale.x = 2;
            }

            transform.localScale = robotScale;
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