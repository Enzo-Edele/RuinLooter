using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int direction;
    RoomTemplates templates;
    List<GameObject> limit = new List<GameObject>();
    bool sansGauche = false;
    bool sansDroite = false;
    bool sansHaut = false;
    bool sansBas = false;
    bool gauche = true;
    bool droit = true;
    bool haut = true;
    bool bas = true;

    Vector3 test;
    int rand;
    public bool isSpawn = false;

    void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.025f);
    }
    void Spawn()
    {
        test = this.transform.position;
        if (isSpawn == false && RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] == false)
        {
            if (test.x + 10.0f >= RoomTemplates.Instance.size * 10 - 30.0f || RoomTemplates.Instance.max[(int)test.x / 10 + 1, (int)test.y / 10] == true)
            {
                sansDroite = true;
            }
            if (test.x - 10.0f <= 20.0f || RoomTemplates.Instance.max[(int)test.x / 10 - 1, (int)test.y / 10] == true)
            {
                sansGauche = true;
            }
            if (test.y + 10.0f>= RoomTemplates.Instance.size * 10 - 30.0f || RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10 + 1] == true)
            {
                sansHaut = true;
            }
            if (test.y - 10.0f <= 20.0f || RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10 - 1] == true)
            {
                sansBas = true;
            }
            switch (direction)
            {
                case 1:
                    //il faut une salle qui s'ouvrent depuis le bas
                    if (sansDroite && sansGauche && sansHaut)
                    {
                        limit.Add(templates.bas);
                    }
                    else if (sansDroite && sansGauche)
                    {
                        limit.Add(templates.basHaut);
                    }
                    else if (sansDroite && sansHaut)
                    {
                        limit.Add(templates.gaucheBas);
                    }
                    else if (sansGauche && sansHaut)
                    {
                        limit.Add(templates.basDroit);
                    }
                    else if (sansDroite)
                    {
                        limit.Add(templates.basHaut);
                        limit.Add(templates.gaucheBas);
                        limit.Add(templates.tGauche);
                    }
                    else if (sansGauche)
                    {
                        limit.Add(templates.basHaut);
                        limit.Add(templates.basDroit);
                        limit.Add(templates.tDroit);
                    }
                    else if (sansHaut)
                    {
                        limit.Add(templates.basDroit);
                        limit.Add(templates.gaucheBas);
                        limit.Add(templates.tBas);
                    }
                    else
                    {
                        for (int i = 0; i < templates.salleBasse.Count; i++)
                        {
                            limit.Add(templates.salleBasse[i]);
                        }
                    }
                    break;
                case 2:
                    //il faut une salle qui s'ouvre depuis la gauche
                    if (sansDroite && sansBas && sansHaut)
                    {
                        limit.Add(templates.gauche);
                    }
                    else if (sansDroite && sansBas)
                    {
                        limit.Add(templates.gaucheHaut);
                    }
                    else if (sansDroite && sansHaut)
                    {
                        limit.Add(templates.gaucheBas);
                    }
                    else if (sansBas && sansHaut)
                    {
                        limit.Add(templates.gaucheDroit);
                    }
                    else if (sansDroite)
                    {
                        limit.Add(templates.gaucheHaut);
                        limit.Add(templates.gaucheBas);
                        limit.Add(templates.tGauche);
                    }
                    else if (sansBas)
                    {
                        limit.Add(templates.gaucheHaut);
                        limit.Add(templates.gaucheDroit);
                        limit.Add(templates.tHaut);
                    }
                    else if (sansHaut)
                    {
                        limit.Add(templates.gaucheDroit);
                        limit.Add(templates.gaucheBas);
                        limit.Add(templates.tBas);
                    }
                    else
                    {
                        for (int i = 0; i < templates.salleGauche.Count; i++)
                        {
                            limit.Add(templates.salleGauche[i]);
                        }
                    }
                    break;
                case 3:
                    //il faut une salle qui s'ouvre depuis le haut
                    if (sansDroite && sansGauche && sansBas)
                    {
                        limit.Add(templates.haut);
                    }
                    else if (sansDroite && sansGauche)
                    {
                        limit.Add(templates.basHaut);
                    }
                    else if (sansDroite && sansBas)
                    {
                        limit.Add(templates.gaucheHaut);
                    }
                    else if (sansGauche && sansBas)
                    {
                        limit.Add(templates.hautDroit);
                    }
                    else if (sansDroite)
                    {
                        limit.Add(templates.basHaut);
                        limit.Add(templates.gaucheHaut);
                        limit.Add(templates.tGauche);
                    }
                    else if (sansGauche)
                    {
                        limit.Add(templates.basHaut);
                        limit.Add(templates.hautDroit);
                        limit.Add(templates.tDroit);
                    }
                    else if (sansBas)
                    {
                        limit.Add(templates.hautDroit);
                        limit.Add(templates.gaucheHaut);
                        limit.Add(templates.tHaut);
                    }
                    else
                    {
                        for (int i = 0; i < templates.salleHaute.Count; i++)
                        {
                            limit.Add(templates.salleHaute[i]);
                        }
                    }
                    break;
                case 4:
                    //il faut une salle qui s'ouvre depuis la droite
                    if (sansBas && sansGauche && sansHaut)
                    {
                        limit.Add(templates.droit);
                    }
                    else if (sansBas && sansGauche)
                    {
                        limit.Add(templates.hautDroit);
                    }
                    else if (sansBas && sansHaut)
                    {
                        limit.Add(templates.gaucheDroit);
                    }
                    else if (sansGauche && sansHaut)
                    {
                        limit.Add(templates.basDroit);
                    }
                    else if (sansBas)
                    {
                        limit.Add(templates.hautDroit);
                        limit.Add(templates.gaucheDroit);
                        limit.Add(templates.tHaut);
                    }
                    else if (sansGauche)
                    {
                        limit.Add(templates.hautDroit);
                        limit.Add(templates.basDroit);
                        limit.Add(templates.tDroit);
                    }
                    else if (sansHaut)
                    {
                        limit.Add(templates.basDroit);
                        limit.Add(templates.gaucheDroit);
                        limit.Add(templates.tBas);
                    }
                    else
                    {
                        for (int i = 0; i < templates.salleDroite.Count; i++)
                        {
                            limit.Add(templates.salleDroite[i]);
                        }
                    }
                    break;
            }
            rand = Random.Range(0, limit.Count);
            Instantiate(limit[rand], transform.position, Quaternion.identity);
            RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] = true;
            isSpawn = true;
        }   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("spawn"))
        {
            if(collision.GetComponent<RoomSpawner>().isSpawn == false && isSpawn == false)
            {
                RaycastHit2D up = Physics2D.Raycast(transform.position, Vector2.up, 6.0f);
                RaycastHit2D down = Physics2D.Raycast(transform.position, Vector2.down, 6.0f);
                RaycastHit2D left = Physics2D.Raycast(transform.position, Vector2.left, 6.0f);
                RaycastHit2D right = Physics2D.Raycast(transform.position, Vector2.right, 6.0f);
                /*if (up || transform.position.y > RoomTemplates.Instance.size * 10 - 30)
                {
                    haut = false;
                    Debug.Log("Raycast haut");
                }
                if (down || transform.position.y < 20.0f)
                {
                    bas = false;
                    Debug.Log("Raycast bas");
                }
                if (left || transform.position.x < 20.0f)
                {
                    gauche = false;
                    Debug.Log("Raycast gauche");
                }
                if (right || transform.position.x > RoomTemplates.Instance.size * 10 - 30)
                {
                    droit = false;
                    Debug.Log("Raycast droit");
                }

                if(haut && bas && gauche && droit)
                {
                    Instantiate(RoomTemplates.Instance.x, transform.position, Quaternion.identity);
                }
                if (haut && bas && gauche)
                {
                    Instantiate(RoomTemplates.Instance.tGauche, transform.position, Quaternion.identity);
                }
                if (haut && bas && droit)
                {
                    Instantiate(RoomTemplates.Instance.tDroit, transform.position, Quaternion.identity);
                }
                if (haut && droit && gauche)
                {
                    Instantiate(RoomTemplates.Instance.tHaut, transform.position, Quaternion.identity);
                }
                if (droit && bas && gauche)
                {
                    Instantiate(RoomTemplates.Instance.tBas, transform.position, Quaternion.identity);
                }
                if (haut && bas)
                {
                    Instantiate(RoomTemplates.Instance.basHaut, transform.position, Quaternion.identity);
                }
                if (haut && gauche)
                {
                    Instantiate(RoomTemplates.Instance.gaucheHaut, transform.position, Quaternion.identity);
                }
                if (haut && droit)
                {
                    Instantiate(RoomTemplates.Instance.hautDroit, transform.position, Quaternion.identity);
                }
                if (gauche && bas)
                {
                    Instantiate(RoomTemplates.Instance.gaucheBas, transform.position, Quaternion.identity);
                }
                if (gauche && droit)
                {
                    Instantiate(RoomTemplates.Instance.gaucheDroit, transform.position, Quaternion.identity);
                }
                if (droit && bas)
                {
                    Instantiate(RoomTemplates.Instance.basDroit, transform.position, Quaternion.identity);
                }
                test = transform.position;
                RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] = true;*/
            }
            isSpawn = true;
        }
    }
}
public enum direction
{
    haut    = 1,
    droite  = 2,
    bas     = 3,
    gauche  = 4,
}
