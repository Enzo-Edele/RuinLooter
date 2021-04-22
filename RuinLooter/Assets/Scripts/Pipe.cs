using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : Trap
{
    //Le Bacquer Alexandre

    [SerializeField]
    GameObject Fumee;

    public float delay = 6;
    private bool shoot = true;
    private Vector3 spawn;
    private float projectileScale;
    private Transform player;
    public AudioClip sound;

    void Awake()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        projectileScale = transform.localScale.x;
        spawn = new Vector3(transform.position.x, transform.position.y + projectileScale/4, transform.position.z);
    }

    public override void LaunchProjectile()
    {
        float distToPlayer = Vector2.Distance(this.transform.position, player.position);
        if (shoot == true && distToPlayer < 15)
        {
            StartCoroutine(Delay());
            GameObject projectileObject = Instantiate(Fumee, spawn + Vector3.up * 0.5f, Quaternion.identity);
            Smoke projectile = projectileObject.GetComponent<Smoke>();
            projectile.Launch(Vector2.up, 0);
            AudioManager.Instance.Playsound(sound, 0.2f);
        }
    }

    IEnumerator Delay()
    {
        shoot = false;
        yield return new WaitForSeconds(delay);
        shoot = true;
    }
}
