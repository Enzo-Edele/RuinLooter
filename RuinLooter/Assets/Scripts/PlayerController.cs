using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;

    int artefact = 0;
    int coin = 0;
    Item powerUp;

    public float timeInvincible = 10.0f;
    public float timerInvincible;

    float horizontal;
    float vertical;
    Vector2 jump = new Vector2(0, 2);
    float jumpforce = 2.0f;
    public bool isOnLadder = false;
    [SerializeField]
    float speed;
    [SerializeField]
    int fullPV = 5;
    public int PV;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        PV = fullPV;
    }
    void Update()
    {
        this.Move();
        if (Input.GetKeyDown("space"))
        {
            this.Jump();
        }
        if(isOnLadder)
        {
            this.UsingLadder();
        }
    }
    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        transform.position = position;
    }
    void Jump() 
    {
        rb2d.AddForce(jump * jumpforce, ForceMode2D.Impulse);
    }
    void UsingLadder()
    {
        vertical = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        position.y = position.y + (speed/2) * vertical * 2 * Time.deltaTime;
        transform.position = position;
    }
    public void Damage(int degat)
    {
        this.PV += degat;
        if(PV > 5)
        {
            PV = 5;
        }
        if (PV < 1)
        {
            Debug.Log("Décés");
        }
    }
    public void CoinCollect()
    {
        coin++;
        UIManager.Instance.UpdateCoin(coin);
    }
    public void ArtefactCollect()
    {
        Debug.Log("Get artefact");
        artefact++;
        if(artefact == 3)
        {
            Debug.Log("Get 3 artefact");
        }
    }
}
public class Item : MonoBehaviour
{
    public virtual void Use()
    {

    }
}
public class Potion : Item
{
    public override void Use()
    {
        base.Use();
    }
}
public class Cape : Item
{
    public override void Use()
    {
        base.Use();
    }
}
public class Bandage : Item
{
    public override void Use()
    {
        base.Use();
    }
}
