using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawn : MonoBehaviour
{
    public GameObject gousset;
    public List<Spawnable> spawnables = new List<Spawnable>();
    int artefact, probabilite;
    Vector2 entityPos;
    void Awake()
    {
        artefact = Random.Range(0, 10);
        if (gousset != null && artefact == 0 && RoomTemplates.Instance.artefactCount < 3)
        {
            Instantiate(gousset, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            RoomTemplates.Instance.artefactCount += 1;
        }
        else
        {
            for (int i = 0; i < spawnables.Count; i++)
            {
                probabilite = Random.Range(0, spawnables[i].uneChanceSur);
                if (probabilite == 0)
                {
                    entityPos.x = spawnables[i].xPos + transform.position.x;
                    entityPos.y = spawnables[i].yPos + transform.position.y;
                    Instantiate(spawnables[i].entity, entityPos, Quaternion.identity);
                    break;
                }
            }
        }
    }
}
[System.Serializable]
public class Spawnable
{
    public GameObject entity;
    public int uneChanceSur;
    public float xPos, yPos;
}