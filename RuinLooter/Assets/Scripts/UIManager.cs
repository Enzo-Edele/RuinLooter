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
        UpdateLevel(SceneManager.GetActiveScene().buildIndex);
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
    public void UpdateLevel(int level)
    {
        Level.text = "Level : " + level;
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
        GameManager.Instance.StaetChange(GameManager.GameState.InGame);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
