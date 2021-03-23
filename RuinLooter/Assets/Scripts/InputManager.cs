using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool pause = false;
    public bool begin = true;//false;
    private static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("InputManager is NULL");
            }
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;
    }
    void Update()
    {
        if (Input.GetKeyDown("escape") && !pause && begin)
        {
            this.PauseState();
        }
        else if(Input.GetKeyDown("escape") && pause && begin)
        {
            this.UnpauseState();
        }
        if(Input.anyKey && !begin)
        {
            //this.StartGame();
        }
    }
    void StartGame()
    {
        begin = true;
        GameManager.Instance.NextLevel();
    }
    public void PauseState()
    {
        GameManager.Instance.StateChange(GameManager.GameState.Pause);
        pause = true;
    }
     public void UnpauseState()
    {
        GameManager.Instance.StateChange(GameManager.GameState.InGame);
        pause = false;
    }
}
