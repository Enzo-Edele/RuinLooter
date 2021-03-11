using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    bool pause = false;
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
        if (Input.GetKeyDown("escape") && !pause)
        {
            this.PauseState();
            pause = false;
        }
        if (Input.GetKeyDown("escape") && pause)
        {
            this.UnpauseState();
            pause = true;
        }
    }
    void PauseState()
    {
        GameManager.Instance.StaetChange(GameManager.GameState.Pause);
    }
    void UnpauseState()
    {
        GameManager.Instance.StaetChange(GameManager.GameState.InGame);
    }
}
