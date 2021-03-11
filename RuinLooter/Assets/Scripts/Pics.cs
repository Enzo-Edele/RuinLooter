using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pics : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage();
    }

    void Damage()
    {
        Debug.Log("dégats");
    }
}
