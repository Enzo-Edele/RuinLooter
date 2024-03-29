using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Cloup Enzo

    public Rigidbody2D rb2d;
    BoxCollider2D bc2d;
    public Animator anim;
    bool isGrounded;
    bool isCrouching = false;
    public int scenePlayer;
    public AudioClip ambianceSound;
    public AudioClip musicMenu;
    public AudioClip musicGame;
    public AudioClip soundTeleportation;
    public AudioClip soundShield;
    public AudioClip soundBandage;
    public AudioClip soundPotion;
    public AudioClip soundHat;
    public AudioClip soundArtefact;
    public AudioClip soundChest;
    public AudioClip hit;

    public int artefact = 0;
    int coin = 0;
    float timeInvincible = 10.0f;
    float timerInvincible;
    bool isInvincible = false;
    [SerializeField]
    GameObject energyShield;
    bool shieldOn = false;
    float timeCloak = 15.0f;
    float timerCloak;
    public bool isCloak = false;
    GameObject artefactTP;

    public float horizontal;
    float vertical;
    Vector3 rightScale = new Vector3(0.65f, 0.65f, 1);
    Vector3 leftScale = new Vector3(-0.65f, 0.65f, 1);
    Vector2 jump = new Vector2(0, 3);
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

    private void Awake()
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
        if (FindObjectOfType<RoomTemplates>() && RoomTemplates.Instance.playerPos != null)
        {
            RoomTemplates.Instance.playerPos.x = transform.position.x;
            RoomTemplates.Instance.playerPos.y = transform.position.y;
        }
        AudioManager.Instance.Playsound(ambianceSound, 0.2f);
        AudioManager.Instance.Playsound(musicMenu, 0.2f);
        AudioManager.Instance.Playsound(musicGame, 0.2f);
        /*
        if (Input.GetKeyDown("m"))
        {
            this.Damage(-1);
        }
        if (Input.GetKeyDown("l"))
        {
            this.Damage(1);
        }
        */
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
                anim.SetBool("IsLaddered", true);
            }
            else
            {
                anim.SetBool("IsLaddered", false);
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

        if (GameManager.Instance._GameState == GameManager.GameState.InGame)
        {
            AudioManager.Instance.StopSound(musicMenu);
            AudioManager.Instance.Playsound(musicGame, 0.2f);
        }
        else
        {
            AudioManager.Instance.StopSound(musicGame);
            AudioManager.Instance.Playsound(musicMenu, 0.2f);
        }
    }
    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        if(horizontal > 0)
        {
            transform.localScale = rightScale;
        }
        else if(horizontal < 0)
        {
            transform.localScale = leftScale;
        }
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
        speed /= 2;
        box.y -= box.y / 1.5f;
        bc2d.size = box;
    }
    void UnCrouch()
    {
        Vector2 box = bc2d.size;
        speed *= 2;
        box.y *= 3;
        bc2d.size = box;
    }
    public void ChestOpenning()
    {
        AudioManager.Instance.Playsound(soundChest, 0.5f);
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
    public string ItemInSlot()
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
                AudioManager.Instance.Playsound(soundHat, 0.2f);
                break;
            case Item.Invincible:
                timerInvincible = timeInvincible;
                isInvincible = true;
                AudioManager.Instance.Playsound(soundPotion, 0.5f);
                break;
            case Item.Heal:
                this.Damage(1);
                AudioManager.Instance.Playsound(soundBandage, 0.2f);
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
                AudioManager.Instance.Playsound(soundTeleportation, 1);
                break;
            case Item.Shield:
                shieldOn = true;
                energyShield.SetActive(true);
                AudioManager.Instance.Playsound(soundShield, 0.1f);
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
            energyShield.SetActive(false);
        }
        else if(degat < 0 && !shieldOn && !isInvincible)
        {
            PV += degat;
            anim.SetTrigger("Hit");
            AudioManager.Instance.Playsound(hit, 0.4f);
        }
        if(PV < 1 && SceneManager.GetActiveScene().buildIndex != 18)
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
        isOnLadder = false;
        anim.SetBool("IsLaddered", false);
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.gravityScale = 1;
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
        AudioManager.Instance.Playsound(soundArtefact, 0.2f);
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
