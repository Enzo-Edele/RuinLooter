using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : Trap
{
    public float delay = 1;
    public GameObject projectilePrefab;
    private bool shoot = true;

    public override void LaunchProjectile()
    {
        if (shoot == true)
        {
            StartCoroutine(Delay());
            GameObject projectileObject = Instantiate(projectilePrefab, transform.position + Vector3.down * 0.5f, Quaternion.identity);
            Arrow projectile = projectileObject.GetComponent<Arrow>();
            projectile.Launch(Vector2.down, 500);
        }
    }

    IEnumerator Delay()
    {
        shoot = false;
        yield return new WaitForSeconds(delay);
        shoot = true;
    }
}
