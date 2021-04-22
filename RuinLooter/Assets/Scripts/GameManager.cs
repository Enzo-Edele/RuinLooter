using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Cloup Enzo

    public int coin;
    public int artefact;
    public int health;
    public string item;
    public enum GameState
    {
        MainMenu,
        InGame,
        Pause,
        Death,
        Victory
    }
    private GameState _gameState;
    public GameState _GameState
    {
        get
        {
            return _gameState;
        }
    }
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is NULL");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        _gameState = GameState.InGame;
    }
    public void StateChange(GameState newState)
    {
        _gameState = newState;
        switch(_gameState)
        {
            case GameState.MainMenu:
                this.MainScreen();
                break;
            case GameState.InGame:
                this.InGame();
                break;
            case GameState.Pause:
                this.Pause();
                break;
            case GameState.Death:
                this.Death();
                break;
            case GameState.Victory:
                this.Victory();
                break;
        }
    }
    void MainScreen()
    {
        health = 5;
        coin = 0;
        artefact = 0;
        item = "Empty";
        Time.timeScale = 0.0f;
        SceneManager.LoadScene("main");
    }
    void InGame()
    {
        Time.timeScale = 1.0f;
        UIManager.Instance.PauseUIOff();
    }
    void Pause()
    {
        Time.timeScale = 0.0f;
        UIManager.Instance.PauseUIOn();
    }
    void Death()
    {
        UIManager.Instance.DeathUI();
        UIManager.Instance.Deactivate();
        SceneManager.LoadScene(18);
        InputManager.Instance.pause = true;
        AudioManager.Instance.StopAllSounds();
    }
    void Victory()
    {
        UIManager.Instance.VictoryUI();
        SceneManager.LoadScene(18);
        InputManager.Instance.pause = true;
    }
    public void UpdateHealth(int change)
    {
        health = change;
        UIManager.Instance.UpdateHealth(health);
    }
    public void UpdateCoin(int change)
    {
        coin = change;
        UIManager.Instance.UpdateCoin(coin);
    }
    public void UpdateArtefact(int change)
    {
        artefact = change;
        UIManager.Instance.UpdateArtefact(artefact);
    }
    public void UpdateItem(string newItem)
    {
        item = newItem;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
