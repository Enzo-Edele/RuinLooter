using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int coin = 0;
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
    }
    public void StaetChange(GameState newState)
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
                break;
            case GameState.Victory:
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
    public void UpdateCoin(int change)
    {
        coin += change;
        UIManager.Instance.UpdateCoin(coin);
    }
}
