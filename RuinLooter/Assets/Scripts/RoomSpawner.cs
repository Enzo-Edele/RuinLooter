using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int direction;
    RoomTemplates templates;
    List<GameObject> limit = new List<GameObject>();
    GameObject minimap;
    bool sansGauche = false;
    bool sansDroit = false;
    bool sansHaut = false;
    bool sansBas = false;
    bool gauche = false;
    bool droit = false;
    bool haut = false;
    bool bas = false;

    Vector3 position;
    string data;
    string testD = "d";
    string testG = "g";
    string testH = "h";
    string testB = "b";

    int rand;
    public bool isSpawn = false;

    void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.025f);
    }
    void Spawn()
    {
        position = this.transform.position;
        if (isSpawn == false && RoomTemplates.Instance.max[(int)position.x / 10, (int)position.y / 10] == null)
        {
            if (position.x + 10.0f >= RoomTemplates.Instance.size * 10 - 10.0f || RoomTemplates.Instance.max[(int)position.x / 10 + 1, (int)position.y / 10] != null)
            {
                sansDroit = true;
            }
            if (position.x - 10.0f <= 0.0f || RoomTemplates.Instance.max[(int)position.x / 10 - 1, (int)position.y / 10] != null)
            {
                sansGauche = true;
            }
            if (position.y + 10.0f>= RoomTemplates.Instance.size * 10 - 10.0f || RoomTemplates.Instance.max[(int)position.x / 10, (int)position.y / 10 + 1] != null)
            {
                sansHaut = true;
            }
            if (position.y - 10.0f <= 0.0f || RoomTemplates.Instance.max[(int)position.x / 10, (int)position.y / 10 - 1] != null)
            {
                sansBas = true;
            }
            switch (direction)
            {
                case 1:
                    //il faut une salle qui s'ouvrent depuis le bas
                    if (sansDroit && sansGauche && sansHaut)
                    {
                        limit.Add(templates.bas);
                    }
                    else if (sansDroit && sansGauche)
                    {
                        limit.Add(templates.basHaut);
                    }
                    else if (sansDroit && sansHaut)
                    {
                        limit.Add(templates.gaucheBas);
                    }
                    else if (sansGauche && sansHaut)
                    {
                        limit.Add(templates.basDroit);
                    }
                    else if (sansDroit)
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
                    if (sansDroit && sansBas && sansHaut)
                    {
                        limit.Add(templates.gauche);
                    }
                    else if (sansDroit && sansBas)
                    {
                        limit.Add(templates.gaucheHaut);
                    }
                    else if (sansDroit && sansHaut)
                    {
                        limit.Add(templates.gaucheBas);
                    }
                    else if (sansBas && sansHaut)
                    {
                        limit.Add(templates.gaucheDroit);
                    }
                    else if (sansDroit)
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
                    if (sansDroit && sansGauche && sansBas)
                    {
                        limit.Add(templates.haut);
                    }
                    else if (sansDroit && sansGauche)
                    {
                        limit.Add(templates.basHaut);
                    }
                    else if (sansDroit && sansBas)
                    {
                        limit.Add(templates.gaucheHaut);
                    }
                    else if (sansGauche && sansBas)
                    {
                        limit.Add(templates.hautDroit);
                    }
                    else if (sansDroit)
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
            if(limit[rand] == templates.bas)
            {
                data = "b";
                minimap = templates.b;
            }
            else if(limit[rand] == templates.haut)
            {
                data = "h";
                minimap = templates.h;
            }
            else if (limit[rand] == templates.droit)
            {
                data = "d";
                minimap = templates.d;
            }
            else if (limit[rand] == templates.gauche)
            {
                data = "g";
                minimap = templates.g;
            }
            else if (limit[rand] == templates.gaucheBas)
            {
                data = "gb";
                minimap = templates.gb;
            }
            else if (limit[rand] == templates.gaucheDroit)
            {
                data = "gd";
                minimap = templates.gd;
            }
            else if (limit[rand] == templates.gaucheHaut)
            {
                data = "gh";
                minimap = templates.gh;
            }
            else if (limit[rand] == templates.basDroit)
            {
                data = "bd";
                minimap = templates.bd;
            }
            else if (limit[rand] == templates.basHaut)
            {
                data = "bh";
                minimap = templates.bh;
            }
            else if (limit[rand] == templates.hautDroit)
            {
                data = "hd";
                minimap = templates.hd;
            }
            else if (limit[rand] == templates.tBas)
            {
                data = "gbd";
                minimap = templates.tb;
            }
            else if (limit[rand] == templates.tHaut)
            {
                data = "ghd";
                minimap = templates.th;
            }
            else if (limit[rand] == templates.tGauche)
            {
                data = "ghb";
                minimap = templates.tg;
            }
            else if (limit[rand] == templates.tDroit)
            {
                data = "dhb";
                minimap = templates.td;
            }
            else if (limit[rand] == templates.x)
            {
                data = "hdbg";
                minimap = templates.xSprite;
            }
            position.x = (int)this.transform.position.x / 10;
            position.y = (int)this.transform.position.y / 10;
            Instantiate(minimap, new Vector2(position.x - 20, position.y), Quaternion.identity);
            RoomTemplates.Instance.max[(int)position.x, (int)position.y] = data;
            isSpawn = true;
        }   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        position.x = (int)this.transform.position.x / 10;
        position.y = (int)this.transform.position.y / 10;
        if (collision.CompareTag("spawn"))
        {
            if (RoomTemplates.Instance.max[(int)position.x, (int)position.y] == null)
            {
                if (transform.position.y < RoomTemplates.Instance.size * 10 - 10 && RoomTemplates.Instance.max[(int)position.x, (int)position.y + 1] != null &&
                        RoomTemplates.Instance.max[(int)position.x, (int)position.y + 1].Contains(testB))
                {
                    haut = true;
                }
                if (transform.position.y > 0.0f && RoomTemplates.Instance.max[(int)position.x, (int)position.y - 1] != null &&
                        RoomTemplates.Instance.max[(int)position.x, (int)position.y - 1].Contains(testH))
                {
                    bas = true;
                }
                if (transform.position.x > 0.0f && RoomTemplates.Instance.max[(int)position.x - 1, (int)position.y] != null &&
                        RoomTemplates.Instance.max[(int)position.x - 1, (int)position.y].Contains(testD))
                {
                    gauche = true;
                }
                if (transform.position.x < RoomTemplates.Instance.size * 10 - 10 && RoomTemplates.Instance.max[(int)position.x + 1, (int)position.y] != null &&
                        RoomTemplates.Instance.max[(int)position.x + 1, (int)position.y].Contains(testG))
                {
                    droit = true;
                }
                if (haut && bas && gauche && droit)
                {
                    Instantiate(RoomTemplates.Instance.x, transform.position, Quaternion.identity);
                    RoomTemplates.Instance.max[(int)position.x, (int)position.y] = "hdbg";
                    Instantiate(templates.xSprite, new Vector2(position.x - 20, position.y), Quaternion.identity);
                }
                else if (haut && bas && gauche)
                {
                    Instantiate(RoomTemplates.Instance.tGauche, transform.position, Quaternion.identity);
                    RoomTemplates.Instance.max[(int)position.x, (int)position.y] = "hgb";
                    Instantiate(templates.tg, new Vector2(position.x - 20, position.y), Quaternion.identity);
                }
                else if (haut && bas && droit)
                {
                    Instantiate(RoomTemplates.Instance.tDroit, transform.position, Quaternion.identity);
                    RoomTemplates.Instance.max[(int)position.x, (int)position.y] = "dhb";
                    Instantiate(templates.td, new Vector2(position.x - 20, position.y), Quaternion.identity);
                }
                else if (haut && droit && gauche)
                {
                    Instantiate(RoomTemplates.Instance.tHaut, transform.position, Quaternion.identity);
                    RoomTemplates.Instance.max[(int)position.x, (int)position.y] = "ghd";
                    Instantiate(templates.th, new Vector2(position.x - 20, position.y), Quaternion.identity);
                }
                else if (droit && bas && gauche)
                {
                    Instantiate(RoomTemplates.Instance.tBas, transform.position, Quaternion.identity);
                    RoomTemplates.Instance.max[(int)position.x, (int)position.y] = "gbd";
                    Instantiate(templates.tb, new Vector2(position.x - 20, position.y), Quaternion.identity);
                }
                else if (haut && bas)
                {
                    Instantiate(RoomTemplates.Instance.basHaut, transform.position, Quaternion.identity);
                    RoomTemplates.Instance.max[(int)position.x, (int)position.y] = "bh";
                    Instantiate(templates.bh, new Vector2(position.x - 20, position.y), Quaternion.identity);
                }
                else if (haut && gauche)
                {
                    Instantiate(RoomTemplates.Instance.gaucheHaut, transform.position, Quaternion.identity);
                    RoomTemplates.Instance.max[(int)position.x, (int)position.y] = "gh";
                    Instantiate(templates.gh, new Vector2(position.x - 20, position.y), Quaternion.identity);
                }
                else if (haut && droit)
                {
                    Instantiate(RoomTemplates.Instance.hautDroit, transform.position, Quaternion.identity);
                    RoomTemplates.Instance.max[(int)position.x, (int)position.y] = "hd";
                    Instantiate(templates.hd, new Vector2(position.x - 20, position.y), Quaternion.identity);
                }
                else if (gauche && bas)
                {
                    Instantiate(RoomTemplates.Instance.gaucheBas, transform.position, Quaternion.identity);
                    RoomTemplates.Instance.max[(int)position.x, (int)position.y] = "gb";
                    Instantiate(templates.gb, new Vector2(position.x - 20, position.y), Quaternion.identity);
                }
                else if (gauche && droit)
                {
                    Instantiate(RoomTemplates.Instance.gaucheDroit, transform.position, Quaternion.identity);
                    RoomTemplates.Instance.max[(int)position.x, (int)position.y] = "gd";
                    Instantiate(templates.gd, new Vector2(position.x - 20, position.y), Quaternion.identity);
                }
                else if (droit && bas)
                {
                    Instantiate(RoomTemplates.Instance.basDroit, transform.position, Quaternion.identity);
                    RoomTemplates.Instance.max[(int)position.x, (int)position.y] = "bd";
                    Instantiate(templates.bd, new Vector2(position.x - 20, position.y), Quaternion.identity);
                }
            }
            isSpawn = true;
        }
    }
}
