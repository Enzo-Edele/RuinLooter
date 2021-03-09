using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text HP;
    [SerializeField]
    TMP_Text Coin;
    [SerializeField]
    TMP_Text Slot;
    [SerializeField]
    TMP_Text Level;

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
    }
    public void UpdateHealth(int health)
    {
        HP.text = "HP : " + health;
    }
    public void UpdateCoin(int coin)
    {
        Coin.text = "Coin : " + coin;
    }
    public void UpdateSlot(string item)
    {
        Slot.text = item;
    }
    public void UpdateLevel(int level)
    {
        Level.text = "Level : " + level;
    }
}
