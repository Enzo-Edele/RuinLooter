using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject HP;
    [SerializeField]
    GameObject HPEgalUn;
    [SerializeField]
    GameObject HPEgalDeux;
    [SerializeField]
    GameObject HPEgalTrois;
    [SerializeField]
    GameObject HPEgalQuatre;
    [SerializeField]
    GameObject HPEgalCinq;
    [SerializeField]
    GameObject HPEgalSix;
    GameObject HPCurrent;
    [SerializeField]
    TMP_Text Coin;
    [SerializeField]
    TMP_Text Artefact;
    [SerializeField]
    TMP_Text Slot;
    [SerializeField]
    TMP_Text Level;
    [SerializeField]
    GameObject fullPopup;
    float timerPopup = 5.0f;
    float timePopup;
    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    GameObject deathScreen;
    [SerializeField]
    GameObject victoryScreen;

    public readonly string tuto = "Il faut récolter les 3 morceaux d'artefact pour reactiver la machine.";
    public readonly string nextLevel = "Vous avez rassembler les artefact";

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is NULL");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        this.Deactivate();
    }
    private void Update()
    {
        if(timePopup >= 0)
        {
            timePopup -= Time.deltaTime;
        }
        else
        {
            if(fullPopup != null)
            {
                fullPopup.SetActive(false);
            }
        }
    }
    public void UpdateAll(int health, int coin, int artefact, string item)
    {
        this.UpdateHealth(health);
        this.UpdateCoin(coin);
        this.UpdateArtefact(artefact);
        this.UpdateSlot(item);
        this.UpdateLevel();
    }
    public void Deactivate()
    {
        if (HP != null)
        {
            HP.SetActive(false);
        }
        Coin.text = "";
        Artefact.text = "";
        Slot.text = "";
        Level.text = "";
        Debug.Log("je desactive");
    }
    public void UpdateHealth(int health)
    {
        if (health != 0)
        {
            HP.SetActive(true);
        }
        if (HPCurrent != null)
        {
            HPCurrent.SetActive(false);
        }
        HPCurrent = null;
        if(health == 5)
        {
            HPCurrent = HPEgalSix;
        }
        if (health == 4)
        {
            HPCurrent = HPEgalCinq;
        }
        if (health == 3)
        {
            HPCurrent = HPEgalQuatre;
        }
        if (health == 2)
        {
            HPCurrent = HPEgalTrois;
        }
        if (health == 1)
        {
            HPCurrent = HPEgalDeux;
        }
        if (health == 0)
        {
            HPCurrent = HPEgalUn;
        }
        HPCurrent.SetActive(true);
        Debug.Log("j'actualise");
    }
    public void UpdateCoin(int coin)
    {
        Coin.text = "Coin : " + coin;
    }
    public void UpdateArtefact(int artefact)
    {
        Artefact.text = "Artefatc : " + artefact;
    }
    public void UpdateSlot(string item)
    {
        Slot.text = item;
    }
    public void UpdateLevel()
    {
        Level.text = "Level : " + SceneManager.GetActiveScene().buildIndex;
    }
    public void Full()
    {
        fullPopup.SetActive(true);
        timePopup = timerPopup;
    }
    public void DeathUI()
    {
        deathScreen.SetActive(true);
    }
    public void VictoryUI()
    {
        victoryScreen.SetActive(true);
    }
    public void PauseUIOn()
    {
        pauseMenu.SetActive(true);
    }
    public void PauseUIOff()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }
    public void ResumeButton()
    {
        InputManager.Instance.UnpauseState();
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
