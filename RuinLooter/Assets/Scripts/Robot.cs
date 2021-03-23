using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Robot : Enemy
{
    
    private Transform player;
    public float agroRange;
    private float spawnPoint;
    public float speed = 0.005f;
    private float speedagro;
    public float distance = 4f;
    public float agroSpeed = 1.2f;
    private float defaultSize;

    private void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        spawnPoint = transform.position.x;
        speedagro = speed;
        defaultSize = transform.localScale.x;
    }

    void Update()
    {
        Physics2D.GetIgnoreLayerCollision(6, 7);
        Vector3 robotScale = transform.localScale;
        transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < agroRange)
        {
            if (transform.position.x < player.position.x)
            {
                robotScale.x = -defaultSize;
                speed = -speedagro * agroSpeed;
            }
            else
            {
                robotScale.x = defaultSize;
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
                robotScale.x = -defaultSize;
            }
            else
            {
                robotScale.x = defaultSize;
            }

            transform.localScale = robotScale;
        }
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