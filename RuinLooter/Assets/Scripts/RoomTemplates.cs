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
    public float speed;
    public GameObject remplis, start, gauche, droit, haut, bas, gaucheBas, gaucheHaut, gaucheDroit, basHaut, basDroit, hautDroit, tHaut, tBas, tDroit, tGauche, x;
    public GameObject spawn, g, d, h, b, gb, gh, gd, bh, bd, hd, th, tb, td, tg, xSprite;
    public List<GameObject> salleHaute, salleBasse, salleDroite, salleGauche;

    public int size;
    public string[,] max;
    [HideInInspector]
    public int artefactCount;
    int spawnX;
    int spawnY;

    bool miniBas = false;
    bool miniHaut = false;
    bool miniGauche = false;
    bool miniDroit = false;
    string testD = "d";
    string testG = "g";
    string testH = "h";
    string testB = "b";
    private void Awake()
    {
        _instance = this;
        artefactCount = 0;
        max = new string[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                max[i, j] = null;
            }
        }
        spawnX = Random.Range(3, size - 3);
        spawnY = Random.Range(2, size - 2);
        Vector2 spawnPos;
        spawnPos.x = spawnX * 10;
        spawnPos.y = spawnY * 10;
        Instantiate(start, spawnPos, Quaternion.identity);
        spawnPos.x = spawnX - 20;
        spawnPos.y = spawnY;
        GameObject test = GameObject.Instantiate(spawn, spawnPos, Quaternion.identity);
        test.transform.SetParent(GameObject.FindGameObjectWithTag("canvas").transform, false);
        max[spawnX, spawnY] = "gd";
        max[spawnX + 1, spawnY] = "gd";
        max[spawnX - 1, spawnY] = "gd";
        Invoke("Reload", speed * 30);

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
        if(artefactCount < 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                if(max[i,j] == null)
                {
                    Instantiate(remplis, new Vector2(i * 10, j * 10), Quaternion.identity);
                }
            }
        }
    }
    public void Minimap()
    {
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                if (max[i, j].Contains(testB))
                {
                    miniHaut = true;
                }
                if (max[i, j].Contains(testH))
                {
                    miniBas = true;
                }
                if (max[i, j].Contains(testD))
                {
                    miniGauche = true;
                }
                if (max[i, j].Contains(testG))
                {
                    miniDroit = true;
                }
                if(miniBas && miniDroit && miniHaut && miniGauche)
                {

                }
                else if(miniBas && miniDroit && miniGauche)
                {

                }
                else if (miniBas && miniDroit && miniHaut)
                {

                }
                else if (miniBas && miniHaut && miniGauche)
                {

                }
                else if (miniHaut && miniDroit && miniGauche)
                {

                }
            }
        }
    }
}
