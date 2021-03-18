using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : Trap
{
    [SerializeField]
    GameObject Fumee;

    public float delay = 6;
    private bool shoot = true;
    private Vector3 spawn;

    private void Start()
    {
        spawn = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
    }

    public override void LaunchProjectile()
    {
        if (shoot == true)
        {
            StartCoroutine(Delay());
            GameObject projectileObject = Instantiate(Fumee, spawn + Vector3.up * 0.5f, Quaternion.identity);
            Smoke projectile = projectileObject.GetComponent<Smoke>();
            projectile.Launch(Vector2.up, 0);
        }
    }

    IEnumerator Delay()
    {
        shoot = false;
        yield return new WaitForSeconds(delay);
        shoot = true;
    }
}
