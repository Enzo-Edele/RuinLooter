using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesla : Trap
{
    [SerializeField]
    Transform player;

    private float playerX;
    private float playerY;
    private float teslaX;
    private float teslaY;
    private Vector2 playerPosition;
    private Vector2 teslaPosition;

    public float teslaBallSpeed = 50;
    public float agroRange;
    public float delay = 3;
    public GameObject projectilePrefab;
    private bool shoot = true;

    private void Start()
    {
        teslaX = gameObject.transform.position.x;
        teslaY = gameObject.transform.position.y;

        teslaPosition = new Vector2(teslaX, teslaY);
    }
    
    public override void LaunchProjectile()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (shoot == true && distToPlayer < agroRange)
        {
            teslaX = gameObject.transform.position.x;
            teslaY = gameObject.transform.position.y;

            teslaPosition = new Vector2(teslaX, teslaY);

            playerX = player.transform.position.x;
            playerY = player.transform.position.y;

            playerPosition = new Vector2(playerX, playerY);

            Vector2 dir = playerPosition - teslaPosition;
            StartCoroutine(Delay());
            GameObject projectileObject = Instantiate(projectilePrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            TeslaBall projectile = projectileObject.GetComponent<TeslaBall>();
            projectile.Launch(dir, teslaBallSpeed);
        }
    }

    IEnumerator Delay()
    {
        shoot = false;
        yield return new WaitForSeconds(delay);
        shoot = true;
    }
}
