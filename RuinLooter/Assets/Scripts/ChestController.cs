using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    bool isOpen = false;
    [SerializeField]
    bool isOn = false;
    private void Update()
    {
        if(Input.GetKeyDown("e") && !isOpen && isOn)
        {
            this.Openning();
        }
    }
    void Openning()
    {
        isOpen = true;
        Debug.Log("Coffre ouvert");
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
