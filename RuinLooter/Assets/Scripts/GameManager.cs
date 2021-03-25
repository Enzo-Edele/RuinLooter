using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int coin = 0;
    public int artefact = 0;
    public int health;
    public enum GameState
    {
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
        InputManager.Instance.pause = true;
    }
    void Victory()
    {
        UIManager.Instance.VictoryUI();
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
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
