using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsSpawner : MonoBehaviour
{
    public List<Props> props = new List<Props>();
    int probabilite;
    Vector2 entityPos;
    void Awake()
    {
        for (int i = 0; i < props.Count; i++)
        {
            probabilite = Random.Range(0, props[i].uneChanceSur);
            if (probabilite == 0)
            {
                entityPos.x = props[i].xPos + transform.position.x;
                entityPos.y = props[i].yPos + transform.position.y;
                Instantiate(props[i].prop, entityPos, Quaternion.identity);
                break;
            }
        }
    }
}
[System.Serializable]
public class Props
{
    public GameObject prop;
    public int uneChanceSur;
    public float xPos, yPos;
}