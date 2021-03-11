using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    bool isOpen = false;
    bool isOn = false;
    private void Update()
    {
        if (Input.GetKeyDown("e") && !isOpen && isOn && GameManager.Instance.coin > 4)
        {
            this.Openning();
        }
    }
    void Openning()
    {
        isOpen = true;
        GameManager.Instance.UpdateCoin(-5);
        Debug.Log("ouverte");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOn = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isOn = false;
    }
}
