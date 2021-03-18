using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LaMontreController : MonoBehaviour
{
    bool isOn = false;
    bool popUpOn = false;
    bool canGoNextLevel = false;
    float timePopUp = 10.0f;
    float timerPopUp;

    [SerializeField]
    GameObject machineDialogue;
    [SerializeField]
    TMP_Text dialogueBox;
    PlayerController player;
    private void Start()
    {
        UIManager.Instance.UpdateLevel();
    }
    private void Update()
    {
        if (Input.GetKeyDown("e") && popUpOn && isOn && canGoNextLevel)
        {
            this.EndingLevel();
        }
        else if (Input.GetKeyDown("e") && !popUpOn && isOn)
        {
            this.DisplayMessage();
        }
        
        if (timerPopUp >= 0)
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
    }
    void DisplayMessage()
    {
        popUpOn = true;
        if(machineDialogue != null)
        {
            machineDialogue.SetActive(true);
            timerPopUp = timePopUp;
            if(player.artefact == 3 || canGoNextLevel)
            {
                dialogueBox.text = UIManager.Instance.nextLevel;
                player.ArtefactCollect(-3);
                canGoNextLevel = true;
            }
            else if(!canGoNextLevel)
            {
                dialogueBox.text = UIManager.Instance.tuto;
            }
        }
    }
    void EndingLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 15)
        {
            UIManager.Instance.VictoryUI();
        }
        else
        {
            GameManager.Instance.NextLevel();
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
