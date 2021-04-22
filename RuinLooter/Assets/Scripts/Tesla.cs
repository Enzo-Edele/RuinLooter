using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesla : Trap
{
    //Le Bacquer Alexandre

    private Transform player;
    public AudioClip shootSound;

    private float playerX;
    private float playerY;
    private float teslaX;
    private float teslaY;
    private Vector2 playerPosition;
    private Vector2 teslaPosition;
    private Vector3 spawn;

    public float teslaBallSpeed = 50;
    private float defaultTeslaBallSpeed;
    public float agroRange;
    public float delay = 3;
    public GameObject projectilePrefab;
    private bool shoot = true;

    private void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        teslaX = this.gameObject.transform.position.x;
        teslaY = this.gameObject.transform.position.y;

        teslaPosition = new Vector2(teslaX, teslaY);
        defaultTeslaBallSpeed = teslaBallSpeed;

        spawn = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    public override void LaunchProjectile()
    {
        float distToPlayer = Vector2.Distance(this.transform.position, player.position);
        teslaBallSpeed = teslaBallSpeed / distToPlayer;

        if (shoot == true && distToPlayer < agroRange && distToPlayer > 3 && player.GetComponent<PlayerController>().isCloak == false)
        {
            playerX = player.transform.position.x;
            playerY = player.transform.position.y;

            playerPosition = new Vector2(playerX, playerY);

            teslaX = this.gameObject.transform.position.x;
            teslaY = this.gameObject.transform.position.y;

            teslaPosition = new Vector2(teslaX, teslaY);

            Vector2 dir = playerPosition - teslaPosition;
            StartCoroutine(Delay());
            GameObject projectileObject = Instantiate(projectilePrefab, spawn + Vector3.up * 0.5f, Quaternion.identity);
            TeslaBall projectile = projectileObject.GetComponent<TeslaBall>();
            projectile.Launch(dir, teslaBallSpeed);
            AudioManager.Instance.Playsound(shootSound, 0.1f);
        }

        teslaBallSpeed = defaultTeslaBallSpeed;
    }

    IEnumerator Delay()
    {
        shoot = false;
        yield return new WaitForSeconds(delay);
        shoot = true;
    }
}