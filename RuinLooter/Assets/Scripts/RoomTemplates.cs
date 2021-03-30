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
    public GameObject start;
    public GameObject gauche, droit, haut, bas, gaucheBas, gaucheHaut, gaucheDroit, basHaut, basDroit, hautDroit, tHaut, tBas, tDroit, tGauche, x;
    public List<GameObject> salleHaute;
    public List<GameObject> salleBasse;
    public List<GameObject> salleDroite;
    public List<GameObject> salleGauche;
    public GameObject roomGauche;
    //public GameObject roomGaucheHaut;
    //public GameObject roomGaucheBas;
    public GameObject roomDroit;
    /*public GameObject roomDroitHaut;
    public GameObject roomDroitBas;
    public List<GameObject> sansHaute;
    public List<GameObject> sansBasse;
    public List<GameObject> sansDroite;
    public List<GameObject> sansGauche;*/

    int height;
    int width;
    public int maxRoom;
    [HideInInspector]
    public int roomCount = 0;
    public int size;
    public bool[,] max;
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
        height = Random.Range(5, size - 5);
        width = Random.Range(5, size - 5);
        Vector2 spawnPos;
        spawnPos.x = height * 10;
        spawnPos.y = width * 10;
        Instantiate(start, spawnPos, Quaternion.identity);
        max[width, height] = true;
        max[width + 1, height] = true;
        max[width - 1, height] = true;
        roomCount += 1;
        Invoke("Reload", 1.0f);
    }
    void Reload()
    {
        if(roomCount < 4)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
