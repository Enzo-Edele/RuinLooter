using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    BoxCollider2D bc2d;
    public Animator anim;
    bool isGrounded;
    bool isCrouching = false;
    public int scenePlayer;
    public AudioClip ambianceSound;
    public AudioClip music;

    public int artefact = 0;
    int coin = 0;
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
    public enum Item
    {
        Empty,
        Cloak,
        Invincible,
        Heal,
        TP,
        Shield
    }
    public Item slot = Item.Empty;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        PV = fullPV;
        UIManager.Instance.UpdateAll(PV, coin, artefact, "Empty");
        scenePlayer = SceneManager.GetActiveScene().buildIndex;
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        AudioManager.Instance.Playsound(ambianceSound, false, 0);
        if (Input.GetKeyDown("m"))
        {
            this.Damage(-1);
        }
        if (Input.GetKeyDown("l"))
        {
            this.Damage(1);
        }
        if (!InputManager.Instance.pause)
        {
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
                anim.SetBool("Crouch", true);
                this.Crouch();
            }
            if (Input.GetKeyUp("c") && isGrounded && isCrouching)
            {
                isCrouching = false;
                anim.SetBool("Crouch", false);
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
        }
    }
    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        anim.SetFloat("Mouv", Mathf.Abs(horizontal));
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        transform.position = position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        anim.SetBool("Ground", true);
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Ghost"))
        {
            Damage(-1);
            StartCoroutine(TimeInvincible());
        }
    }
    void Jump() 
    {
        if (isGrounded)
        {
            rb2d.AddForce(jump * jumpforce, ForceMode2D.Impulse);
            isGrounded = false;
            anim.SetBool("Ground", false);
            if (isCrouching)
            {
                isCrouching = false;
                anim.SetBool("Crouch", false);
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
        box.y -= box.y / 2;
        bc2d.size = box;
    }
    void UnCrouch()
    {
        Vector2 box = bc2d.size;
        box.y *= 2;
        bc2d.size = box;
    }
    public void ChestOpenning()
    {
        int dice = Random.Range(1, 101);
        if (dice >= 1 && dice < 21)
        {
            slot = Item.Cloak;
        }
        if (dice >= 21 && dice < 41)
        {
            slot = Item.Invincible;
        }
        if (dice >= 41 && dice < 66)
        {
            slot = Item.Heal;
        }
        if (dice >= 66 && dice < 81)
        {
            slot = Item.TP;
        }
        if (dice >= 81 && dice <= 100)
        {
            slot = Item.Shield;
        }
        UIManager.Instance.UpdateSlot(ItemInSlot());
    }
    string ItemInSlot()
    {
        switch(slot)
        {
            case Item.Cloak:
                return "Cape";
            case Item.Invincible:
                return "Potion";
            case Item.Heal:
                return "Bandage";
            case Item.TP:
                return "Pearl";
            case Item.Shield:
                return "Shield";
            default:
                return "Empty";
        }
    }
    public Item ItemSave(string item)
    {
        switch (item)
        {
            case "Cape":
                return Item.Cloak;
            case "Potion":
                return Item.Invincible;
            case "Bandage":
                return Item.Heal;
            case "Pearl":
                return Item.TP;
            case "Shield":
                return Item.Shield;
            default:
                return Item.Empty;
        }
    }
    void UseItem()
    {
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
        slot = Item.Empty;
        UIManager.Instance.UpdateSlot(ItemInSlot());
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
    public void PlacementNewLevel(float coorX, float coorY)
    {
        Vector2 position = transform.position;
        position.x = coorX;
        position.y = coorY;
        transform.position = position;
        PV = 0;
        coin = 0;
        artefact = 0;
        slot = ItemSave(GameManager.Instance.item);
        this.Damage(GameManager.Instance.health);
        this.CoinCollect(GameManager.Instance.coin);
        this.ArtefactCollect(GameManager.Instance.artefact);
        UIManager.Instance.UpdateAll(PV, coin, artefact, ItemInSlot());
    }
    public void PrepareNewLevel()
    {
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }
    public IEnumerator TimeInvincible()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(7, 8, true);
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }
}
