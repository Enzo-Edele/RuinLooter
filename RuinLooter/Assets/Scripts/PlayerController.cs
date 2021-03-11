using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    bool isGrounded;
    bool isCrouching = false;

    int artefact = 0;
    int coin = 0;
    Item powerUp;
    public float timeInvincible = 10.0f;
    public float timerInvincible;
    bool isInvincible = false;
    bool shieldOn = false;

    float horizontal;
    float vertical;
    Vector2 jump = new Vector2(0, 2);
    float jumpforce = 2.0f;
    public bool isOnLadder = false;
    [SerializeField]
    float speed;
    int fullPV = 5;
    int PV;
    enum Item
    {
        Empty,
        Cloak,
        Invincible,
        Heal,
        TP,
        Shield
    }
    Item slot = Item.Empty;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        PV = fullPV;
        UIManager.Instance.UpdateHealth(PV);
        UIManager.Instance.UpdateCoin(coin);
        UIManager.Instance.UpdateArtefact(artefact);
        UIManager.Instance.UpdateSlot("Empty");
    }
    void Update()
    {
        this.Move();
        if(Input.GetKeyDown("space"))
        {
            this.Jump();
        }
        if(isOnLadder)
        {
            this.UsingLadder();
        }
        if(Input.GetKeyDown("c"))
        {
            isCrouching = true;
            this.Crouch();
        }
        if(Input.GetKeyUp("c"))
        {
            isCrouching = false;
        }
        if(Input.GetKeyDown("a"))
        {
            this.UseItem();
        }
        if(timerInvincible >= 0)
        {
            timerInvincible -= Time.deltaTime;
        }
        else
        {
            isInvincible = false;
        }
    }
    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        transform.position = position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
    void Jump() 
    {
        if (isGrounded)
        {
            rb2d.AddForce(jump * jumpforce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    void UsingLadder()
    {
        vertical = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        position.y = position.y + (speed/2) * vertical * 2 * Time.deltaTime;
        transform.position = position;
    }
    void Crouch()
    {
        if (isGrounded)
        {
            Debug.Log("crouching");
        }
    }
    public void ChestOpenning()
    {
        int dice = Random.Range(1, 6);
        if(dice == 1)
        {
            slot = Item.Cloak;
            UIManager.Instance.UpdateSlot("Cape");
        }
        if (dice == 2)
        {
            slot = Item.Invincible;
            UIManager.Instance.UpdateSlot("Potion");
        }
        if (dice == 3)
        {
            slot = Item.Heal;
            UIManager.Instance.UpdateSlot("Bandage");
        }
        if (dice == 4)
        {
            slot = Item.TP;
            UIManager.Instance.UpdateSlot("Pearl");
        }
        if (dice == 5)
        {
            slot = Item.Shield;
            UIManager.Instance.UpdateSlot("Shield");
        }
    }
    void UseItem()
    {
        UIManager.Instance.UpdateSlot("Empty");
        switch(slot)
        {
            case Item.Cloak:
                Debug.Log("Use Cape");
                break;
            case Item.Invincible:
                timerInvincible = timeInvincible;
                isInvincible = true;
                Debug.Log("Use Potion");
                break;
            case Item.Heal:
                this.Damage(1);
                Debug.Log("Use Bandage");
                break;
            case Item.TP:
                Debug.Log("Use Pearl");
                break;
            case Item.Shield:
                shieldOn = true;
                Debug.Log("Use Shield");
                break;
        }
    }
    public void Damage(int degat)
    {
        if(degat > 0 && PV < 5)
        {
            PV += degat;
        }
       else if(degat < 0 && shieldOn && !isInvincible)
        {
            shieldOn = false;
        }
        else if(degat < 0 && !shieldOn && !isInvincible)
        {
            PV += degat;
        }
        if(PV < 1)
        {
            Debug.Log("Il est daicerdé");
            Destroy(gameObject);
        }

        UIManager.Instance.UpdateHealth(PV);
    }
    public void CoinCollect()
    {
        coin++;
        GameManager.Instance.UpdateCoin(1);
    }
    public void ArtefactCollect()
    {
        artefact++;
        UIManager.Instance.UpdateArtefact(artefact);
        if(artefact == 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
