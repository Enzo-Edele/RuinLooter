using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LaMontreController : MonoBehaviour
{
    // Cloup Enzo

    bool isOn = false;
    bool popUpOn = false;
    bool canGoNextLevel = false;
    float timePopUp = 5.0f;
    float timerPopUp = 0;
    float timeAnim = 2.0f;
    float timerAnim = 0;
    public float x;
    public float y;
    int endLevel = 15;

    SpriteRenderer inUse;
    [SerializeField]
    Sprite repair;
    [SerializeField]
    GameObject machineDialogue;
    [SerializeField]
    TMP_Text dialogueBox;
    PlayerController player;
    private void Start()
    {
        UIManager.Instance.UpdateLevel();
        inUse = GetComponent<SpriteRenderer>();
        Vector2 pos = transform.position;
        x = pos.x;
        y = pos.y;
        player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        player.PlacementNewLevel(pos.x,pos.y);
        player = null;
    }
    private void Update()
    {
        /*
        if (Input.GetKeyDown("n"))
        {
            this.EndingLevel();
        }
        */
        if (Input.GetKeyDown("e") && popUpOn && isOn && canGoNextLevel)
        {
            this.EndingLevel();
        }
        else if (Input.GetKeyDown("e") && !popUpOn && isOn)
        {
            this.DisplayMessage();
        }
        if (timerPopUp > 0)
        {
            timerPopUp -= Time.deltaTime;
        }
        else
        {
            if (machineDialogue != null)
            {
                machineDialogue.SetActive(false);
            }
            popUpOn = false;
        }
        if (timerAnim > 0)
        {
            timerAnim -= Time.deltaTime;
        }
        else if(timerAnim < 0)
        {
            UIManager.Instance.ActiveLoad();
            GameManager.Instance.NextLevel();
        }
    }
    void DisplayMessage()
    {
        popUpOn = true;
        if(machineDialogue != null)
        {
            machineDialogue.SetActive(true);
            timerPopUp = timePopUp;
            if(player.artefact == 3)
            {
                dialogueBox.text = UIManager.Instance.nextLevel;
                player.ArtefactCollect(-3);
                player.PrepareNewLevel();
                canGoNextLevel = true;
                inUse.sprite = repair;
            }
            else if(canGoNextLevel)
            {
                dialogueBox.text = UIManager.Instance.nextLevel;
            }
            else if(!canGoNextLevel)
            {
                dialogueBox.text = UIManager.Instance.tuto;
            }
        }
    }
    void EndingLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == endLevel + 1)
        {
            UIManager.Instance.VictoryUI();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 17)
        {
            UIManager.Instance.MainMenuButton();
        }
        else
        {
            UIManager.Instance.LaunchEndLevelAnimation();
            timerAnim = timeAnim;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        isOn = true;
        player = other.GetComponent<PlayerController>();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isOn = false;
        player = null;
    }
}
