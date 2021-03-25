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
    Vector3 test;
    int rand;
    bool isSpawn = false;

    void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.05f);
    }
    void Spawn()
    {
        if(isSpawn == false)
        {
            if (direction == 1)
            {
                //il faut une salle qui s'ouvrent depuis le bas
                test.x = this.transform.position.x + 10.0f;
                test.y = this.transform.position.y;
                if (test.x / 10 >= 17.0f || RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] == true)
                {
                    Debug.Log("Check 1");
                    sansDroite = true;
                }
                test.x = this.transform.position.x - 10.0f;
                test.y = this.transform.position.y;
                if (test.x / 10 <= 2.0f || RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] == true)
                {
                    Debug.Log("Check 2");
                    sansGauche = true;
                };
                test.x = this.transform.position.x;
                test.y = this.transform.position.y + 10.0f;
                if (test.y / 10 >= 17.0f || RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] == true)
                {
                    Debug.Log("Check 3");
                    sansHaut = true;
                }
                if(sansDroite && sansGauche && sansHaut)
                {
                    for(int i = 0; i < templates.sansDroite.Count; i++)
                    {
                        for(int j = 0; j < templates.sansGauche.Count; j++)
                        {
                            for(int k = 0; k < templates.sansHaute.Count; k++)
                            {
                                for(int l = 0; l < templates.salleBasse.Count; l++)
                                {
                                    if(templates.salleBasse[l] == templates.sansHaute[k] == templates.sansGauche[j] == templates.sansDroite[i])
                                    {
                                        limit.Add(templates.salleBasse[l]);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (sansDroite && sansGauche)
                {
                    for (int i = 0; i < templates.sansGauche.Count; i++)
                    {
                        for (int j = 0; j < templates.sansDroite.Count; j++)
                        {
                            for (int k = 0; k < templates.salleBasse.Count; k++)
                            {
                                if (templates.salleBasse[k] == templates.sansDroite[j] == templates.sansGauche[i])
                                {
                                    limit.Add(templates.salleBasse[k]);
                                }
                            }
                        }
                    }
                }
                else if (sansDroite && sansHaut)
                {
                    for (int i = 0; i < templates.sansHaute.Count; i++)
                    {
                        for (int j = 0; j < templates.sansDroite.Count; j++)
                        {
                            for (int k = 0; k < templates.salleBasse.Count; k++)
                            {
                                if (templates.salleBasse[k] == templates.sansDroite[j] == templates.sansHaute[i])
                                {
                                    limit.Add(templates.salleBasse[k]);
                                }
                            }
                        }
                    }
                }
                else if (sansGauche && sansHaut)
                {
                    for (int i = 0; i < templates.sansGauche.Count; i++)
                    {
                        for (int j = 0; j < templates.sansHaute.Count; j++)
                        {
                            for (int k = 0; k < templates.salleBasse.Count; k++)
                            {
                                if (templates.salleBasse[k] == templates.sansHaute[j] == templates.sansGauche[i])
                                {
                                    limit.Add(templates.salleBasse[k]);
                                }
                            }
                        }
                    }
                }
                else if (sansDroite)
                {
                    for (int i = 0; i < templates.sansDroite.Count; i++)
                    {
                        for (int j = 0; j < templates.salleBasse.Count; j++)
                        {
                            if (templates.salleBasse[j] == templates.sansDroite[i])
                            {
                                limit.Add(templates.salleBasse[j]);
                            }
                        }
                    }
                }
                else if (sansGauche)
                {
                    for (int i = 0; i < templates.sansGauche.Count; i++)
                    {
                        for (int j = 0; j < templates.salleBasse.Count; j++)
                        {
                            if (templates.salleBasse[j] == templates.sansGauche[i])
                            {
                                limit.Add(templates.salleBasse[j]);
                            }
                        }
                    }
                }
                else if (sansHaut)
                {
                    for (int i = 0; i < templates.sansHaute.Count; i++)
                    {
                        for (int j = 0; j < templates.salleBasse.Count; j++)
                        {
                            if (templates.salleBasse[j] == templates.sansHaute[i])
                            {
                                limit.Add(templates.salleBasse[j]);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < templates.salleBasse.Count; i++)
                    {
                        limit.Add(templates.salleBasse[i]);
                    }
                }
                rand = Random.Range(0, limit.Count);
                Instantiate(limit[rand], transform.position, limit[rand].transform.rotation);
                RoomTemplates.Instance.count += 1;
                test = this.transform.position;
                RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] = true;
            }
            else if (direction == 2)
            {
                //il faut une salle qui s'ouvre depuis la gauche
                test.x = this.transform.position.x + 10.0f;
                test.y = this.transform.position.y;
                if (test.x / 10 >= 17.0f || RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] == true)
                {
                    Debug.Log("Check 4");
                    sansDroite = true;
                }
                test.x = this.transform.position.x;
                test.y = this.transform.position.y - 10.0f;
                if (test.y / 10 <= 2.0f || RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] == true)
                {
                    Debug.Log("Check 5");
                    sansBas = true;
                };
                test.x = this.transform.position.x;
                test.y = this.transform.position.y + 10.0f;
                if (test.y / 10 >= 17.0f || RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] == true)
                {
                    Debug.Log("Check 6");
                    sansHaut = true;
                }
                test.x = this.transform.position.x + 10.0f;
                test.y = this.transform.position.y;
                if (test.x / 10 <= 14.0f && RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] == false && RoomTemplates.Instance.max[(int)test.x / 10 + 1, (int)test.y / 10] == false
                        && RoomTemplates.Instance.max[(int)test.x / 10 + 2, (int)test.y / 10] == false)
                {
                    limit.Add(RoomTemplates.Instance.roomGauche);
                    if (test.y < 18.0f && RoomTemplates.Instance.max[(int)test.x / 10 + 1, (int)test.y / 10 + 1] == false)
                    {
                        limit.Add(RoomTemplates.Instance.roomGaucheHaut);
                    }
                    else if (test.y > 1.0f && RoomTemplates.Instance.max[(int)test.x / 10 + 1, (int)test.y / 10 - 1] == false)
                    {
                        limit.Add(RoomTemplates.Instance.roomGaucheBas);
                    }
                }
                if (sansDroite && sansBas && sansHaut)
                {
                    for (int i = 0; i < templates.sansDroite.Count; i++)
                    {
                        for (int j = 0; j < templates.sansBasse.Count; j++)
                        {
                            for (int k = 0; k < templates.sansHaute.Count; k++)
                            {
                                for (int l = 0; l < templates.salleGauche.Count; l++)
                                {
                                    if (templates.salleGauche[l] == templates.sansHaute[k] == templates.sansBasse[j] == templates.sansDroite[i])
                                    {
                                        limit.Add(templates.salleGauche[l]);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (sansDroite && sansBas)
                {
                    for (int i = 0; i < templates.sansBasse.Count; i++)
                    {
                        for (int j = 0; j < templates.sansDroite.Count; j++)
                        {
                            for (int k = 0; k < templates.salleGauche.Count; k++)
                            {
                                if (templates.salleGauche[k] == templates.sansDroite[j] == templates.sansBasse[i])
                                {
                                    limit.Add(templates.salleGauche[k]);
                                }
                            }
                        }
                    }
                }
                else if (sansDroite && sansHaut)
                {
                    for (int i = 0; i < templates.sansHaute.Count; i++)
                    {
                        for (int j = 0; j < templates.sansDroite.Count; j++)
                        {
                            for (int k = 0; k < templates.salleGauche.Count; k++)
                            {
                                if (templates.salleGauche[k] == templates.sansDroite[j] == templates.sansHaute[i])
                                {
                                    limit.Add(templates.salleGauche[k]);
                                }
                            }
                        }
                    }
                }
                else if (sansBas && sansHaut)
                {
                    for (int i = 0; i < templates.sansBasse.Count; i++)
                    {
                        for (int j = 0; j < templates.sansHaute.Count; j++)
                        {
                            for (int k = 0; k < templates.salleGauche.Count; k++)
                            {
                                if (templates.salleGauche[k] == templates.sansHaute[j] == templates.sansBasse[i])
                                {
                                    limit.Add(templates.salleGauche[k]);
                                }
                            }
                        }
                    }
                }
                else if (sansDroite)
                {
                    for (int i = 0; i < templates.sansDroite.Count; i++)
                    {
                        for (int j = 0; j < templates.salleGauche.Count; j++)
                        {
                            if (templates.salleGauche[j] == templates.sansDroite[i])
                            {
                                limit.Add(templates.salleGauche[j]);
                            }
                        }
                    }
                }
                else if (sansBas)
                {
                    for (int i = 0; i < templates.sansBasse.Count; i++)
                    {
                        for (int j = 0; j < templates.salleGauche.Count; j++)
                        {
                            if (templates.salleGauche[j] == templates.sansBasse[i])
                            {
                                limit.Add(templates.salleGauche[j]);
                            }
                        }
                    }
                }
                else if (sansHaut)
                {
                    for (int i = 0; i < templates.sansHaute.Count; i++)
                    {
                        for (int j = 0; j < templates.salleGauche.Count; j++)
                        {
                            if (templates.salleGauche[j] == templates.sansHaute[i])
                            {
                                limit.Add(templates.salleGauche[j]);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < templates.salleGauche.Count; i++)
                    {
                        limit.Add(templates.salleGauche[i]);
                    }
                }
                rand = Random.Range(0, limit.Count);
                Instantiate(limit[rand], transform.position, limit[rand].transform.rotation);
                RoomTemplates.Instance.count += 1;
                test = this.transform.position;
                RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] = true;
                if (limit[rand] == RoomTemplates.Instance.roomGauche || limit[rand] == RoomTemplates.Instance.roomGaucheHaut || limit[rand] == RoomTemplates.Instance.roomGaucheBas)
                {
                    RoomTemplates.Instance.max[(int)test.x / 10 + 1, (int)test.y / 10] = true;
                    RoomTemplates.Instance.max[(int)test.x / 10 + 2, (int)test.y / 10] = true;
                }
            }
            else if (direction == 3)
            {
                //il faut une salle qui s'ouvre depuis le haut
                test.x = this.transform.position.x + 10.0f;
                test.y = this.transform.position.y;
                if (test.x / 10 >= 17.0f || RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] == true)
                {
                    Debug.Log("Check 7");
                    sansDroite = true;
                }
                test.x = this.transform.position.x - 10.0f;
                test.y = this.transform.position.y;
                if (test.x / 10 <= 2.0f || RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] == true)
                {
                    Debug.Log("Check 8");
                    sansGauche = true;
                };
                test.x = this.transform.position.x;
                test.y = this.transform.position.y - 10.0f;
                if (test.y / 10 <= 2.0f || RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] == true)
                {
                    Debug.Log("Check 9");
                    sansBas = true;
                }
                if (sansDroite && sansGauche && sansBas)
                {
                    for (int i = 0; i < templates.sansDroite.Count; i++)
                    {
                        for (int j = 0; j < templates.sansGauche.Count; j++)
                        {
                            for (int k = 0; k < templates.sansBasse.Count; k++)
                            {
                                for (int l = 0; l < templates.salleHaute.Count; l++)
                                {
                                    if (templates.salleHaute[l] == templates.sansBasse[k] == templates.sansGauche[j] == templates.sansDroite[i])
                                    {
                                        limit.Add(templates.salleHaute[l]);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (sansDroite && sansGauche)
                {
                    for (int i = 0; i < templates.sansGauche.Count; i++)
                    {
                        for (int j = 0; j < templates.sansDroite.Count; j++)
                        {
                            for (int k = 0; k < templates.salleHaute.Count; k++)
                            {
                                if (templates.salleHaute[k] == templates.sansDroite[j] == templates.sansGauche[i])
                                {
                                    limit.Add(templates.salleHaute[k]);
                                }
                            }
                        }
                    }
                }
                else if (sansDroite && sansBas)
                {
                    for (int i = 0; i < templates.sansBasse.Count; i++)
                    {
                        for (int j = 0; j < templates.sansDroite.Count; j++)
                        {
                            for (int k = 0; k < templates.salleHaute.Count; k++)
                            {
                                if (templates.salleHaute[k] == templates.sansDroite[j] == templates.sansBasse[i])
                                {
                                    limit.Add(templates.salleHaute[k]);
                                }
                            }
                        }
                    }
                }
                else if (sansGauche && sansBas)
                {
                    for (int i = 0; i < templates.sansGauche.Count; i++)
                    {
                        for (int j = 0; j < templates.sansBasse.Count; j++)
                        {
                            for (int k = 0; k < templates.salleHaute.Count; k++)
                            {
                                if (templates.salleHaute[k] == templates.sansBasse[j] == templates.sansGauche[i])
                                {
                                    limit.Add(templates.salleHaute[k]);
                                }
                            }
                        }
                    }
                }
                else if (sansDroite)
                {
                    for (int i = 0; i < templates.sansDroite.Count; i++)
                    {
                        for (int j = 0; j < templates.salleHaute.Count; j++)
                        {
                            if (templates.salleHaute[j] == templates.sansDroite[i])
                            {
                                limit.Add(templates.salleHaute[j]);
                            }
                        }
                    }
                }
                else if (sansGauche)
                {
                    for (int i = 0; i < templates.sansGauche.Count; i++)
                    {
                        for (int j = 0; j < templates.salleHaute.Count; j++)
                        {
                            if (templates.salleHaute[j] == templates.sansGauche[i])
                            {
                                limit.Add(templates.salleHaute[j]);
                            }
                        }
                    }
                }
                else if (sansBas)
                {
                    for (int i = 0; i < templates.sansBasse.Count; i++)
                    {
                        for (int j = 0; j < templates.salleHaute.Count; j++)
                        {
                            if (templates.salleHaute[j] == templates.sansBasse[i])
                            {
                                limit.Add(templates.salleHaute[j]);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < templates.salleHaute.Count; i++)
                    {
                        limit.Add(templates.salleHaute[i]);
                    }
                }
                rand = Random.Range(0, limit.Count);
                Instantiate(limit[rand], transform.position, limit[rand].transform.rotation);
                RoomTemplates.Instance.count += 1;
                test = this.transform.position;
                RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] = true;
            }
            else if (direction == 4)
            {
                //il faut une salle qui s'ouvre depuis la droite
                test.x = this.transform.position.x;
                test.y = this.transform.position.y - 10.0f;
                if (test.y / 10 <= 2.0f || RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] == true)
                {
                    Debug.Log("Check 10");
                    sansBas = true;
                }
                test.x = this.transform.position.x - 10.0f;
                test.y = this.transform.position.y;
                if (test.x / 10 <= 2.0f || RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] == true)
                {
                    Debug.Log("Check 11");
                    sansGauche = true;
                };
                test.x = this.transform.position.x;
                test.y = this.transform.position.y + 10.0f;
                if (test.y / 10 >= 17.0f || RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] == true)
                {
                    Debug.Log("Check 12");
                    sansHaut = true;
                }
                test.x = this.transform.position.x - 10.0f;
                test.y = this.transform.position.y;
                if (test.x / 10 >= 5.0f && RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] == false && RoomTemplates.Instance.max[(int)test.x / 10 - 1, (int)test.y / 10] == false
                        && RoomTemplates.Instance.max[(int)test.x / 10 - 2, (int)test.y / 10] == false)
                {
                    limit.Add(RoomTemplates.Instance.roomDroit);
                    if (test.y < 18.0f && RoomTemplates.Instance.max[(int)test.x / 10 - 1, (int)test.y / 10 + 1] == false)
                    {
                        limit.Add(RoomTemplates.Instance.roomDroitHaut);
                    }
                    else if (test.y > 1.0f && RoomTemplates.Instance.max[(int)test.x / 10 - 1, (int)test.y / 10 - 1] == false)
                    {
                        limit.Add(RoomTemplates.Instance.roomDroitBas);
                    }
                }
                if (sansBas && sansGauche && sansHaut)
                {
                    for (int i = 0; i < templates.sansBasse.Count; i++)
                    {
                        for (int j = 0; j < templates.sansGauche.Count; j++)
                        {
                            for (int k = 0; k < templates.sansHaute.Count; k++)
                            {
                                for (int l = 0; l < templates.salleDroite.Count; l++)
                                {
                                    if (templates.salleDroite[l] == templates.sansHaute[k] == templates.sansGauche[j] == templates.sansBasse[i])
                                    {
                                        limit.Add(templates.salleDroite[l]);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (sansBas && sansGauche)
                {
                    for (int i = 0; i < templates.sansGauche.Count; i++)
                    {
                        for (int j = 0; j < templates.sansBasse.Count; j++)
                        {
                            for (int k = 0; k < templates.salleDroite.Count; k++)
                            {
                                if (templates.salleDroite[k] == templates.sansBasse[j] == templates.sansGauche[i])
                                {
                                    limit.Add(templates.salleDroite[k]);
                                }
                            }
                        }
                    }
                }
                else if (sansBas && sansHaut)
                {
                    for (int i = 0; i < templates.sansHaute.Count; i++)
                    {
                        for (int j = 0; j < templates.sansBasse.Count; j++)
                        {
                            for (int k = 0; k < templates.salleDroite.Count; k++)
                            {
                                if (templates.salleDroite[k] == templates.sansBasse[j] == templates.sansHaute[i])
                                {
                                    limit.Add(templates.salleDroite[k]);
                                }
                            }
                        }
                    }
                }
                else if (sansGauche && sansHaut)
                {
                    for (int i = 0; i < templates.sansGauche.Count; i++)
                    {
                        for (int j = 0; j < templates.sansHaute.Count; j++)
                        {
                            for (int k = 0; k < templates.salleDroite.Count; k++)
                            {
                                if (templates.salleDroite[k] == templates.sansHaute[j] == templates.sansGauche[i])
                                {
                                    limit.Add(templates.salleDroite[k]);
                                }
                            }
                        }
                    }
                }
                else if (sansBas)
                {
                    for (int i = 0; i < templates.sansBasse.Count; i++)
                    {
                        for (int j = 0; j < templates.salleDroite.Count; j++)
                        {
                            if (templates.salleDroite[j] == templates.sansBasse[i])
                            {
                                limit.Add(templates.salleDroite[j]);
                            }
                        }
                    }
                }
                else if (sansGauche)
                {
                    for (int i = 0; i < templates.sansGauche.Count; i++)
                    {
                        for (int j = 0; j < templates.salleDroite.Count; j++)
                        {
                            if (templates.salleDroite[j] == templates.sansGauche[i])
                            {
                                limit.Add(templates.salleDroite[j]);
                            }
                        }
                    }
                }
                else if (sansHaut)
                {
                    for (int i = 0; i < templates.sansHaute.Count; i++)
                    {
                        for (int j = 0; j < templates.salleDroite.Count; j++)
                        {
                            if (templates.salleDroite[j] == templates.sansHaute[i])
                            {
                                limit.Add(templates.salleDroite[j]);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < templates.salleDroite.Count; i++)
                    {
                        limit.Add(templates.salleDroite[i]);
                    }
                }
                rand = Random.Range(0, limit.Count);
                Instantiate(limit[rand], transform.position, limit[rand].transform.rotation);
                RoomTemplates.Instance.count += 1;
                test = this.transform.position;
                RoomTemplates.Instance.max[(int)test.x / 10, (int)test.y / 10] = true;
                if(limit[rand] == RoomTemplates.Instance.roomDroit || limit[rand] == RoomTemplates.Instance.roomDroitHaut || limit[rand] == RoomTemplates.Instance.roomDroitBas)
                {
                    RoomTemplates.Instance.max[(int)test.x / 10 - 1, (int)test.y / 10] = true;
                    RoomTemplates.Instance.max[(int)test.x / 10 - 2, (int)test.y / 10] = true;
                }
            }
            isSpawn = true;
        }   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("spawn"))
        {
            if(collision.GetComponent<RoomSpawner>().isSpawn == false && isSpawn == false)
            {
                
                Destroy(gameObject);
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
