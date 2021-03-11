using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance
    {
        get
        {
            if (_instance == null)
            {
                
            }
            return _instance;
        }
    }
    public GameObject spawnRoom;
    public List<Room> rooms = null;
    public static int count;
    void Awake()
    {
        _instance = this;
        Vector2 spawnPos = transform.position;
        spawnPos.x = 0.0f;
        spawnPos.y = 0.0f;
        spawnRoom = Instantiate(spawnRoom, spawnPos, Quaternion.identity);
        count = 0;
        SpawnRoom(rooms[0]);
    }

    private void SpawnRoom(Room room)
    {
        if (count > 4)
        {
            return;
        }

        int randomRoom = Random.Range(0, room.rooms.Count - 1);
        Room newRoom = room.rooms[randomRoom];
        for(int i=0; i < newRoom.entries.Count; i++)
        {
            for(int j=0; j < room.exits.Count; j++)
            {
                if(newRoom.entries[i].IsDoorValid(room.exits[j]))
                {
                    Room newRoomObject = Instantiate(newRoom, newRoom.entries[i].position + room.transform.position, Quaternion.identity);
                    SpawnRoom(newRoomObject);
                    count += 1;
                    break;
                }
            }
        }  
    }
}
