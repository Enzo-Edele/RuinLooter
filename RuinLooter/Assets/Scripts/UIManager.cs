using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text HP;
    [SerializeField]
    TMP_Text Coin;
    [SerializeField]
    TMP_Text Artefact;
    [SerializeField]
    TMP_Text Slot;
    [SerializeField]
    TMP_Text Level;
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
    public void UpdateAll(int health, int coin, int artefact, string item)
    {
        this.UpdateHealth(health);
        this.UpdateCoin(coin);
        this.UpdateArtefact(artefact);
        this.UpdateSlot(item);
        this.UpdateLevel();
    }
    void Deactivate()
    {
        HP.text = "";
        Coin.text = "";
        Artefact.text = "";
        Slot.text = "";
        Level.text = "";
    }
    public void UpdateHealth(int health)
    {
        HP.text = "HP : " + health;
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
