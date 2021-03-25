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
    public List<GameObject> salleHaute;
    public List<GameObject> salleBasse;
    public List<GameObject> salleDroite;
    public List<GameObject> salleGauche;
    public GameObject roomGauche;
    public GameObject roomGaucheHaut;
    public GameObject roomGaucheBas;
    public GameObject roomDroit;
    public GameObject roomDroitHaut;
    public GameObject roomDroitBas;
    public List<GameObject> sansHaute;
    public List<GameObject> sansBasse;
    public List<GameObject> sansDroite;
    public List<GameObject> sansGauche;

    int height;
    int width;
    public int count;
    public bool[,] max = new bool[20, 20];
    private void Awake()
    {
        _instance = this;
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                max[i, j] = false;
            }
        }
        height = Random.Range(5, 15);
        width = Random.Range(5, 15);
        Vector2 spawnPos;
        spawnPos.x = height * 10;
        spawnPos.y = width * 10;
        Instantiate(start, spawnPos, Quaternion.identity);
        count += 1;
        max[width, height] = true;
        Invoke("Reload", 2.0f);
    }
    void Reload()
    {
        if(count < 100)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
