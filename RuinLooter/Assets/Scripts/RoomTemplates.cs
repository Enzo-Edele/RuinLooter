using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTemplates : MonoBehaviour
{
    private static RoomTemplates _instance;
    public static RoomTemplates Instance
    {
        get
        {
            if (_instance == null)
            {

            }
            return _instance;
        }
    }
    public GameObject start, gauche, droit, haut, bas, gaucheBas, gaucheHaut, gaucheDroit, basHaut, basDroit, hautDroit, tHaut, tBas, tDroit, tGauche, x;
    public List<GameObject> salleHaute, salleBasse, salleDroite, salleGauche;

    public int size;
    public bool[,] max;
    int spawnX;
    int spawnY;
    private void Awake()
    {
        _instance = this;
        max = new bool[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                max[i, j] = false;
            }
        }
        spawnX = Random.Range(5, size - 5);
        spawnY = Random.Range(5, size - 5);
        Vector2 spawnPos;
        spawnPos.x = spawnX * 10;
        spawnPos.y = spawnY * 10;
        Instantiate(start, spawnPos, Quaternion.identity);
        max[spawnX, spawnY] = true;
        max[spawnX + 1, spawnY] = true;
        max[spawnX - 1, spawnY] = true;
    }
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void Reload()
    {
        /*if(roomCount < 4)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }*/
    }
}
