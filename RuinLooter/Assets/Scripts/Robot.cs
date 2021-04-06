using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Robot : MonoBehaviour
{
    
    private Transform player;
    public float agroRange;
    private float spawnPoint;
    public float defaultSpeed = 0.005f;
    private float speed;
    public float agroSpeed = 1.2f;
    public float distance = 4f;
    private float defaultSize;
    public AudioClip bip;

    private void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        spawnPoint = transform.position.x;
        speed = defaultSpeed;
        defaultSize = transform.localScale.x;
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        //AudioManager.Instance.Playsound(bip, true, distToPlayer);
        if (transform.position.x.ToString("0000.00") != player.transform.position.x.ToString("0000.00"))
        {
            Vector3 robotScale = transform.localScale;
            transform.position = new Vector3(transform.position.x - speed * Time.timeScale, transform.position.y, transform.position.z);

            if (distToPlayer <= agroRange)
            {
                if (transform.position.x < player.position.x)
                {
                    robotScale.x = -defaultSize;
                    speed = -defaultSpeed * agroSpeed;
                }
                else
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
        }
    }
}