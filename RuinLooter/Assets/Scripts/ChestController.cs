using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    bool isOpen = false;
    bool isOn = false;
    PlayerController player;
    Animator anim;

    float timeAnim = 0.8f;
    float timerAnim;
    bool isMove = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(Input.GetKeyDown("e") && !isOpen && isOn)
        {
            this.Openning();
        }
        if(timerAnim > 0)
        {
            timerAnim -= Time.deltaTime;
        }
        if(timerAnim < 0 && !isMove)
        {
            Vector2 position = transform.position;
            position.y += 0.23f;
            transform.position = position;
            isMove = true;
        }
    }
    void Openning()
    {
        if(player != null)
        {
            if(player.slot == PlayerController.Item.Empty)
            {
                player.ChestOpenning();
                anim.SetTrigger("Openning");
                isOpen = true;
                timerAnim = timeAnim;
            }
            else
            {
                anim.SetTrigger("FullOpen");
                UIManager.Instance.Full();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        isOn = true;
        player = other.GetComponent<PlayerController>();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isOn = false;
        player = null;
    }
}
