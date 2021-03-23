using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    BoxCollider2D bc2d;
    bool isGrounded;
    bool isCrouching = false;

    public int artefact = 0;
    int coin = 0;
    Item powerUp;
    float timeInvincible = 10.0f;
    float timerInvincible;
    bool isInvincible = false;
    bool shieldOn = false;
    float timeCloak = 15.0f;
    float timerCloak;
    public bool isCloak = false;
    GameObject artefactTP;

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
        bc2d = GetComponent<BoxCollider2D>();
        PV = fullPV;
        UIManager.Instance.UpdateAll(PV, coin, artefact, "Empty");
    }
    void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            this.Damage(-1);
        }
        /*
        if (!InputManager.Instance.pause)
        {
        */
            this.Move();
            if (Input.GetKeyDown("space"))
            {
                this.Jump();
            }
            if (isOnLadder)
            {
                this.UsingLadder();
            }
            if (Input.GetKeyDown("c") && isGrounded && !isCrouching)
            {
                isCrouching = true;
                this.Crouch();
            }
            if (Input.GetKeyUp("c") && isGrounded && isCrouching)
            {
                isCrouching = false;
                this.UnCrouch();
            }
            if (Input.GetKeyDown("a"))
            {
                this.UseItem();
            }
            if (timerInvincible >= 0)
            {
                timerInvincible -= Time.deltaTime;
            }
            else
            {
                isInvincible = false;
            }
            if (timerCloak >= 0)
            {
                timerCloak -= Time.deltaTime;
            }
            else
            {
                isCloak = false;
            }
            /*
        }*/
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
            if (isCrouching)
            {
                isCrouching = false;
                this.UnCrouch();
            }
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
        Vector2 box = bc2d.size;
        Debug.Log(box.x + ", " + box.y);
        box.y -= box.y / 2;
        bc2d.size = box;
    }
    void UnCrouch()
    {
        Vector2 box = bc2d.size;
        Debug.Log(box.x + ", " + box.y);
        box.y *= 2;
        bc2d.size = box;
    }
    public void ChestOpenning()
    {
        int dice = Random.Range(1, 101);
        if(dice >= 1 && dice < 21)
        {
            slot = Item.Cloak;
            UIManager.Instance.UpdateSlot("Cape");
        }
        if (dice >= 21 && dice < 41)
        {
            slot = Item.Invincible;
            UIManager.Instance.UpdateSlot("Potion");
        }
        if (dice >= 41 && dice < 66)
        {
            slot = Item.Heal;
            UIManager.Instance.UpdateSlot("Bandage");
        }
        if (dice >= 66 && dice < 81)
        {
            slot = Item.TP;
            UIManager.Instance.UpdateSlot("Pearl");
        }
        if (dice >= 81 && dice < 100)
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
                timerCloak = timeCloak;
                isCloak = true;
                break;
            case Item.Invincible:
                timerInvincible = timeInvincible;
                isInvincible = true;
                break;
            case Item.Heal:
                this.Damage(1);
                break;
            case Item.TP:
                ArtefactController anchor = FindObjectOfType(typeof(ArtefactController)) as ArtefactController;
                if (anchor != null)
                {
                    Vector2 positionTP = new Vector2(anchor.x - 1, anchor.y);
                    transform.position = positionTP;
                }
                else
                {
                    LaMontreController gate = FindObjectOfType(typeof(LaMontreController)) as LaMontreController;
                    Vector2 positionTP = new Vector2(gate.x, gate.y);
                    transform.position = positionTP;
                }
                break;
            case Item.Shield:
                shieldOn = true;
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
            GameManager.Instance.StateChange(GameManager.GameState.Death);
        }
        GameManager.Instance.UpdateHealth(PV);
    }
    public void CoinCollect(int change)
    {
        coin += change;
        GameManager.Instance.UpdateCoin(coin);
    }
    public void ArtefactCollect(int change)
    {
        artefact += change;
        GameManager.Instance.UpdateArtefact(artefact);
    }
}
