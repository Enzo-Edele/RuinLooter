using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Room> rooms = null;
    public Room end = null;
    public List<Door> entries = new List<Door>();
    public List<Door> exits = new List<Door>();
    //public List<Wall> wall = new List<Wall>();
    public int count;
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
/*public class Wall
{
    public DoorType type;
    
    public bool IsWall(Door door)
    {

        return true;
    }
}*/

public enum DoorType
{
    Haut = -2,
    Gauche = -1,
    Droit  = 1,
    Bas = 2,
}