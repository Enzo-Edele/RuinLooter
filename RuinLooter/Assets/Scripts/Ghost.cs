using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    //Le Bacquer Alexandre

    Vector2 spawnPoint;
    public float speed = 2f;
    public float moveZone = 10;
    private float ghostPositionX;
    private float ghostPositionY;
    public float delay = 5;
    private bool ghostMove = true;
    Rigidbody2D rb2d;
        private Transform player;
    public AudioClip sound;

    void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

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
        float distToPlayer = Vector2.Distance(this.transform.position, player.position);
        if (ghostMove == true)
        {
            rb2d.AddForce(dir * 30);
            StartCoroutine(Move());
            if (distToPlayer < 15)
            {
                AudioManager.Instance.Playsound(sound, 0.2f);
            }
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
}
