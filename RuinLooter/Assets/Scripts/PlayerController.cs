using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    int artefact = 0;
    int coin = 0;

    float horizontal;
    float vertical;
    Vector2 jump = new Vector2(0, 2);
    float jumpforce = 2.0f;
    [SerializeField]
    float speed;
    public int PV;
    int fullPV = 5;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        PV = fullPV;
    }
    void Update()
    {
        this.Move();
        this.Jump();
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
        if(Input.GetKeyDown("space"))
        {
            rb2d.AddForce(jump * jumpforce, ForceMode2D.Impulse);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        vertical = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        position.y = position.y + speed * vertical * 2 * Time.deltaTime;
        transform.position = position;
    }
    public void CoinCollect()
    {
        Debug.Log("coin");
        coin++;
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
