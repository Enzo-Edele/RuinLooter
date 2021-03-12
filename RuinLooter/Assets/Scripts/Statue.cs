using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : Trap
{
    [SerializeField]
    Transform player;

    private float playerX;
    private float playerY;
    private float statueX;
    private float statueY;
    private Vector2 playerPosition;
    private Vector2 statuePosition;

    public float fireBallSpeed = 50;
    public float agroRange;
    public int delay = 3;
    public GameObject projectilePrefab;
    private bool shoot = true;

    private void Start()
    {
        statueX = gameObject.transform.position.x;
        statueY = gameObject.transform.position.y;

        statuePosition = new Vector2(statueX, statueY);
    }
    
    public override void LaunchProjectile()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (shoot == true && distToPlayer < agroRange)
        {
            statueX = gameObject.transform.position.x;
            statueY = gameObject.transform.position.y;

            statuePosition = new Vector2(statueX, statueY);

            playerX = player.transform.position.x;
            playerY = player.transform.position.y;

            playerPosition = new Vector2(playerX, playerY);

            Vector2 dir = playerPosition - statuePosition;
            StartCoroutine(Delay());
            GameObject projectileObject = Instantiate(projectilePrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            FireBall projectile = projectileObject.GetComponent<FireBall>();
            projectile.Launch(dir, fireBallSpeed);
        }
    }

    IEnumerator Delay()
    {
        shoot = false;
        yield return new WaitForSeconds(delay);
        shoot = true;
    }
}
