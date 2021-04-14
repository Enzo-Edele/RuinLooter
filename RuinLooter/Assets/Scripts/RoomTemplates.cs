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
    public GameObject spawn, sg, sd, g, d, h, b, gb, gh, gd, bh, bd, hd, th, tb, td, tg, xSprite, r;
    public GameObject pointeur;
    public List<GameObject> salleHaute, salleBasse, salleDroite, salleGauche;
    GameObject minimap;

    public int size;
    public string[,] max;
    [HideInInspector]
    public int artefactCount;
    [HideInInspector]
    public Vector2 playerPos;
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
        max[spawnX, spawnY] = "gd";
        max[spawnX + 1, spawnY] = "gd";
        max[spawnX - 1, spawnY] = "gd";
        Invoke("Reload", speed * 30);
        Invoke("Minimap", speed * 32);
        GameObject p = Instantiate(pointeur, new Vector2(1480, 800), Quaternion.identity, GameObject.FindGameObjectWithTag("Minimap").transform);
    }
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        playerPos.x = (int)playerPos.x / 10 + 1480;
        playerPos.y = (int)playerPos.y / 10 + 800;
        pointeur.transform.position = playerPos;
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
                if (max[i, j] != null && max[i, j].Contains(testB))
                {
                    miniBas = true;
                }
                if (max[i, j] != null && max[i, j].Contains(testH))
                {
                    miniHaut = true;
                }
                if (max[i, j] != null && max[i, j].Contains(testD))
                {
                    miniDroit = true;
                }
                if (max[i, j] != null && max[i, j].Contains(testG))
                {
                    miniGauche = true;
                }

                if (i == spawnX && j == spawnY)
                {
                    minimap = spawn;
                }
                else if (i == spawnX + 1 && j == spawnY)
                {
                    minimap = sd;
                }
                else if (i == spawnX - 1 && j == spawnY)
                {
                    minimap = sg;
                }
                else if (miniBas && miniDroit && miniHaut && miniGauche)
                {
                    minimap = xSprite;
                }
                else if (miniBas && miniDroit && miniGauche)
                {
                    minimap = tb;
                }
                else if (miniBas && miniDroit && miniHaut)
                {
                    minimap = td;
                }
                else if (miniBas && miniHaut && miniGauche)
                {
                    minimap = tg;
                }
                else if (miniHaut && miniDroit && miniGauche)
                {
                    minimap = th;
                }
                else if (miniBas && miniDroit)
                {
                    minimap = bd;
                }
                else if (miniBas && miniGauche)
                {
                    minimap = gb;
                }
                else if (miniBas && miniHaut)
                {
                    minimap = bh;
                }
                else if (miniHaut && miniDroit)
                {
                    minimap = hd;
                }
                else if (miniHaut && miniGauche)
                {
                    minimap = gh;
                }
                else if (miniGauche && miniDroit)
                {
                    minimap = gd;
                }
                else if (miniBas)
                {
                    minimap = b;
                }
                else if (miniDroit)
                {
                    minimap = d;
                }
                else if (miniGauche)
                {
                    minimap = g;
                }
                else if (miniHaut)
                {
                    minimap = h;
                }
                else
                {
                    minimap = r;
                }
                GameObject miniInstance = Instantiate(minimap, new Vector2(i * 20 + 1480, j * 20 + 800), Quaternion.identity, GameObject.FindGameObjectWithTag("Minimap").transform);
                miniBas = false;
                miniDroit = false;
                miniGauche = false;
                miniHaut = false;
            }
        }
    }
}
