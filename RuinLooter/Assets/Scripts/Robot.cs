using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Robot : MonoBehaviour
{
    //Le Bacquer Alexandre

    private float raycastSize = 0;
    private int collide = 1;
    private Transform player;
    public float agroRange;
    private float spawnPoint;
    public float defaultSpeed = 0.005f;
    private float speed;
    public float agroSpeed = 1.2f;
    public float distance = 4f;
    private float defaultSize;
    public AudioClip bip;
    int roomLayer;

    private void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        roomLayer = LayerMask.GetMask("Room");
        spawnPoint = transform.position.x;
        speed = defaultSpeed;
        defaultSize = transform.localScale.x;
    }

    void FixedUpdate()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (transform.position.x.ToString("0000.00") != player.transform.position.x.ToString("0000.00"))
        {
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 1, roomLayer);
            if (hitDown.collider == null)
            {
                raycastSize = 0.3f;
            }
            else
            {
                raycastSize = 0;
            }
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, raycastSize, roomLayer);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, raycastSize, roomLayer);

            Vector3 robotScale = transform.localScale;
            transform.position = new Vector3(transform.position.x - speed * Time.timeScale * collide, transform.position.y, transform.position.z);

            if (distToPlayer <= agroRange && player.GetComponent<PlayerController>().isCloak == false)
            {
                AudioManager.Instance.Playsound(bip, 0.2f);
                if (transform.position.x < player.position.x)
                {
                    robotScale.x = -defaultSize;
                    speed = -defaultSpeed * agroSpeed;
                }
                else if (transform.position.x >= player.position.x)
                {
                    robotScale.x = defaultSize;
                    speed = defaultSpeed * agroSpeed;
                }
            }

            else 
            {
                if (transform.position.x < spawnPoint - distance)
                {
                    spawnPoint = transform.position.x;
                    robotScale.x = -defaultSize;
                    speed = -defaultSpeed;
                }
                else if (transform.position.x > spawnPoint + distance)
                {
                    spawnPoint = transform.position.x;
                    robotScale.x = defaultSize;
                    speed = defaultSpeed;
                }
            }

            transform.localScale = robotScale;

            if (hitLeft.collider != null || hitRight.collider != null)
            {
                collide = 0;
            }
            else
            {
                collide = 1;
            }
        }
    }
}