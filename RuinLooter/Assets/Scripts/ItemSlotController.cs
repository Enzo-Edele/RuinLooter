using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotController : MonoBehaviour
{
    // Cloup Enzo

    Image inUse;
    public Sprite actualSprite; 
    void Start()
    {
        inUse = GetComponent<Image>();
    }
    void Update()
    {
        inUse.sprite = actualSprite;
    }
}
