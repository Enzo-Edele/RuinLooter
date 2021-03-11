using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Room> rooms = null;
    public List<Door> entries = new List<Door>();
    public List<Door> exits = new List<Door>();
    public Vector2 position;
    private void Awake()
    {
        position = transform.position;
    }
}
[System.Serializable]
public class Door
{
    public Vector3 position; 
    public DoorType type;

    public bool IsDoorValid(Door door)
    {
        int result = (int)this.type + (int)door.type;
        return result == 0;
    }
}

public enum DoorType
{
    Haut = -2,
    Gauche = -1,
    Droit  = 1,
    Bas = 2,
}