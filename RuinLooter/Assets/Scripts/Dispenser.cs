using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : Trap
{
    //Le Bacquer Alexandre

    public float delay = 1;
    public GameObject projectilePrefab;
    private bool shoot = true;
    private Transform player;
    public AudioClip sound;

    void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void LaunchProjectile()
    {
        float distToPlayer = Vector2.Distance(this.transform.position, player.position);
        if (shoot == true && distToPlayer < 15)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, transform.position + Vector3.down, Quaternion.identity);
            Acid projectile = projectileObject.GetComponent<Acid>();
            projectile.Launch(Vector2.down, 400);
            AudioManager.Instance.Playsound(sound, 0.2f);
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        shoot = false;
        yield return new WaitForSeconds(delay);
        shoot = true;
    }
}
